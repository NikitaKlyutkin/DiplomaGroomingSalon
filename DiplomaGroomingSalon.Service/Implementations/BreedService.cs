using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Service.Interfaces;

namespace DiplomaGroomingSalon.Service.Implementations;

public class BreedService : ICRUDDataService<Breed>
{
	private readonly IBaseRepository<Breed> _breedRepository;
	private readonly IBaseRepository<ServiceType> _serviceTypeRepository;

	public BreedService(IBaseRepository<Breed> breedRepository,
		IBaseRepository<ServiceType> serviceTypeRepository)
	{
		_breedRepository = breedRepository;
		_serviceTypeRepository = serviceTypeRepository;
	}

	public async Task<IBaseResponse<List<Breed>>> GetAll()
	{
		try
		{
			var breeds = await _breedRepository.GetAll();
			if (!breeds.Any())
				return new BaseResponse<List<Breed>>
				{
					Description = "Found 0 items",
					StatusCode = StatusCode.OK
				};
			return new BaseResponse<List<Breed>>
			{
				Data = breeds.ToList(),
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<List<Breed>>
			{
				Description = $"[GetBreeds] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<Breed>> GetById(Guid id)
	{
		try
		{
			var breed = await _breedRepository.GetByIdAsync(id);
			if (breed == null)
				return new BaseResponse<Breed>
				{
					Description = "Not Found",
					StatusCode = StatusCode.NotFound
				};

			var data = new Breed
			{
				Id = breed.Id,
				BreedName = breed.BreedName,
				PetType = breed.PetType,
				PetTypeId = breed.PetTypeId
			};

			return new BaseResponse<Breed>
			{
				StatusCode = StatusCode.OK,
				Data = data
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<Breed>
			{
				Description = $"[GetBreed] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<Breed>> Create(Breed model)
	{
		var baseResponse = new BaseResponse<Breed>();
		try
		{
			var breed = new Breed
			{
				Id = new Guid(),
				PetTypeId = model.PetTypeId,
				BreedName = model.BreedName
			};

			await _breedRepository.Create(breed);
		}
		catch (Exception ex)
		{
			return new BaseResponse<Breed>
			{
				Description = $"[CreateBreed]: {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}

		return baseResponse;
	}

	public async Task<IBaseResponse<Breed>> Edit(Guid id, Breed model)
	{
		try
		{
			var breedAsync = await _breedRepository.GetByIdAsync(id);
			if (breedAsync == null)
				return new BaseResponse<Breed>
				{
					Description = "Not found",
					StatusCode = StatusCode.NotFound
				};
			breedAsync.Id = model.Id;
			breedAsync.BreedName = model.BreedName;
			breedAsync.PetTypeId = model.PetTypeId;

			await _breedRepository.Update(breedAsync);


			return new BaseResponse<Breed>
			{
				Data = breedAsync,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<Breed>
			{
				Description = $"[EditBreed] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<bool>> Delete(Guid id)
	{
		try
		{
			var breedAsync = await _breedRepository.GetByIdAsync(id);
			if (breedAsync == null)
				return new BaseResponse<bool>
				{
					Description = "Not found",
					StatusCode = StatusCode.NotFound,
					Data = false
				};

			var serviceType = _serviceTypeRepository.GetAll().Result.FirstOrDefault(x => x.BreedId == id);
			if (serviceType != null) await _serviceTypeRepository.DeleteRange(serviceType);

			await _breedRepository.Delete(breedAsync);


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
				Description = $"[DeleteBreed] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}
}