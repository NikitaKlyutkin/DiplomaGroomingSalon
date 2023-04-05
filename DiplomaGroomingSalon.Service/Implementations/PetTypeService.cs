using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Service.Interfaces;

namespace DiplomaGroomingSalon.Service.Implementations;

public class PetTypeService : ICRUDDataService<PetType>
{
	private readonly IBaseRepository<Breed> _breedRepository;
	private readonly IBaseRepository<PetType> _petTypePetRepository;
	private readonly IBaseRepository<ServiceType> _serviceTypeRepository;

	public PetTypeService(IBaseRepository<PetType> petTypePetRepository, IBaseRepository<Breed> breedRepository,
		IBaseRepository<ServiceType> serviceTypeRepository)
	{
		_petTypePetRepository = petTypePetRepository;
		_breedRepository = breedRepository;
		_serviceTypeRepository = serviceTypeRepository;
	}

	public async Task<IBaseResponse<List<PetType>>> GetAll()
	{
		try
		{
			var petTypes = await _petTypePetRepository.GetAll();

			if (!petTypes.Any())
				return new BaseResponse<List<PetType>>
				{
					Description = "Found 0 items",
					StatusCode = StatusCode.OK
				};
			return new BaseResponse<List<PetType>>
			{
				Data = petTypes.ToList(),
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<List<PetType>>
			{
				Description = $"[GetTypePets] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<PetType>> GetById(Guid id)
	{
		try
		{
			var petType = await _petTypePetRepository.GetByIdAsync(id);
			if (petType == null)
				return new BaseResponse<PetType>
				{
					Description = "Not Found",
					StatusCode = StatusCode.UserNotFound
				};

			var data = new PetType
			{
				Id = petType.Id,
				PetTypeName = petType.PetTypeName
			};

			return new BaseResponse<PetType>
			{
				StatusCode = StatusCode.OK,
				Data = data
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<PetType>
			{
				Description = $"[GetTypePet] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<PetType>> Create(PetType model)
	{
		var baseResponse = new BaseResponse<PetType>();
		try
		{
			var petType = new PetType
			{
				Id = Guid.NewGuid(),
				PetTypeName = model.PetTypeName
			};

			await _petTypePetRepository.Create(petType);
		}
		catch (Exception ex)
		{
			return new BaseResponse<PetType>
			{
				Description = $"[CreateTypePet]: {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}

		return baseResponse;
	}

	public async Task<IBaseResponse<PetType>> Edit(Guid id, PetType model)
	{
		try
		{
			var petType = await _petTypePetRepository.GetByIdAsync(id);
			if (petType == null)
				return new BaseResponse<PetType>
				{
					Description = "Not found",
					StatusCode = StatusCode.NotFound
				};
			petType.Id = model.Id;
			petType.PetTypeName = model.PetTypeName;

			await _petTypePetRepository.Update(petType);


			return new BaseResponse<PetType>
			{
				Data = petType,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<PetType>
			{
				Description = $"[EditTypePet] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<bool>> Delete(Guid id)
	{
		try
		{
			var petType = await _petTypePetRepository.GetByIdAsync(id);
			if (petType == null)
				return new BaseResponse<bool>
				{
					Description = "Not found",
					StatusCode = StatusCode.NotFound,
					Data = false
				};

			var breed = _breedRepository.GetAll().Result.FirstOrDefault(x => x.PetTypeId == id);
			var serviceType = _serviceTypeRepository.GetAll().Result.FirstOrDefault(x => x.PetTypeId == id);
			if (serviceType != null) await _serviceTypeRepository.DeleteRange(serviceType);

			if (breed != null) await _breedRepository.DeleteRange(breed);

			await _petTypePetRepository.Delete(petType);


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
				Description = $"[DeleteTypePet] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}
}