using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Service.Interfaces;

namespace DiplomaGroomingSalon.Service.Implementations;

public class ServiceTypeService : ICRUDDataService<ServiceType>
{
	private readonly IBaseRepository<ServiceType> _serviceTypeRepository;

	public ServiceTypeService(IBaseRepository<ServiceType> serviceTypeRepository)
	{
		_serviceTypeRepository = serviceTypeRepository;
	}

	public async Task<IBaseResponse<List<ServiceType>>> GetAll()
	{
		try
		{
			var serviceTypes = await _serviceTypeRepository.GetAll();

			if (!serviceTypes.Any())
				return new BaseResponse<List<ServiceType>>
				{
					Description = "Found 0 items",
					StatusCode = StatusCode.OK
				};
			return new BaseResponse<List<ServiceType>>
			{
				Data = serviceTypes.ToList(),
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<List<ServiceType>>
			{
				Description = $"[GetServiceTypes] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<ServiceType>> GetById(Guid id)
	{
		try
		{
			var serviceType = await _serviceTypeRepository.GetByIdAsync(id);
			if (serviceType == null)
				return new BaseResponse<ServiceType>
				{
					Description = "Not Found",
					StatusCode = StatusCode.NotFound
				};

			var data = new ServiceType
			{
				Id = serviceType.Id,
				ServiceTypeName = serviceType.ServiceTypeName,
				Breed = serviceType.Breed,
				PetTypeId = serviceType.PetTypeId,
				BreedId = serviceType.BreedId,
				Price = serviceType.Price
			};

			return new BaseResponse<ServiceType>
			{
				StatusCode = StatusCode.OK,
				Data = data
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<ServiceType>
			{
				Description = $"[GetServiceType] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<ServiceType>> Create(ServiceType model)
	{
		var baseResponse = new BaseResponse<ServiceType>();
		try
		{
			var serviceType = new ServiceType
			{
				Id = new Guid(),
				BreedId = model.BreedId,
				PetTypeId = model.PetTypeId,
				Price = model.Price,
				ServiceTypeName = model.ServiceTypeName
			};

			await _serviceTypeRepository.Create(serviceType);
		}
		catch (Exception ex)
		{
			return new BaseResponse<ServiceType>
			{
				Description = $"[CreateServiceType]: {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}

		return baseResponse;
	}

	public async Task<IBaseResponse<ServiceType>> Edit(Guid id, ServiceType model)
	{
		try
		{
			var serviceType = await _serviceTypeRepository.GetByIdAsync(id);
			if (serviceType == null)
				return new BaseResponse<ServiceType>
				{
					Description = "Not found",
					StatusCode = StatusCode.NotFound
				};
			serviceType.Id = model.Id;
			serviceType.PetTypeId = model.PetTypeId;
			serviceType.BreedId = model.BreedId;
			serviceType.ServiceTypeName = model.ServiceTypeName;
			serviceType.Price = model.Price;

			await _serviceTypeRepository.Update(serviceType);


			return new BaseResponse<ServiceType>
			{
				Data = serviceType,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<ServiceType>
			{
				Description = $"[EditServiceType] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<bool>> Delete(Guid id)
	{
		try
		{
			var serviceType = await _serviceTypeRepository.GetByIdAsync(id);
			if (serviceType == null)
				return new BaseResponse<bool>
				{
					Description = "Not found",
					StatusCode = StatusCode.NotFound,
					Data = false
				};
			await _serviceTypeRepository.Delete(serviceType);


			return new BaseResponse<bool>
			{
				Data = true,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<bool>
			{
				Description = $"[DeleteServiceType] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}
}