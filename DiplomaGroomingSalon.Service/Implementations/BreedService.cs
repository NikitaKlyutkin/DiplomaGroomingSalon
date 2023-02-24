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
		var baseResponse = new BaseResponse<IEnumerable<Breed>>();
		try
		{
			var breedPets = await _breedRepository.GetAll();
			if (!breedPets.Any())
				return new BaseResponse<List<Breed>>
				{
					Description = "Found 0 items",
					StatusCode = StatusCode.OK
				};
			return new BaseResponse<List<Breed>>
			{
				Data = breedPets.ToList(),
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<List<Breed>>
			{
				Description = $"[GetBreedPets] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<Breed>> GetById(Guid id)
	{
		try
		{
			var breedpet = await _breedRepository.GetByIdAsync(id);
			if (breedpet == null)
				return new BaseResponse<Breed>
				{
					Description = "Not Found",
					StatusCode = StatusCode.NotFound
				};

			var data = new Breed
			{
				Id = breedpet.Id,
				breedPetName = breedpet.breedPetName,
				PetTypeId = breedpet.PetTypeId,
				PetTypeName = breedpet.PetType.typePetName
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
				Description = $"[GetBreedPet] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<Breed>> Create(Breed model)
	{
		var baseResponse = new BaseResponse<Breed>();
		try
		{
			var breedPet = new Breed
			{
				Id = new Guid(),
				PetTypeId = model.PetTypeId,
				breedPetName = model.breedPetName
			};

			await _breedRepository.Create(breedPet);
		}
		catch (Exception ex)
		{
			return new BaseResponse<Breed>
			{
				Description = $"[CreateBreedPet]: {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}

		return baseResponse;
	}

	public async Task<IBaseResponse<Breed>> Edit(Guid id, Breed model)
	{
		try
		{
			var breedpet = await _breedRepository.GetByIdAsync(id);
			if (breedpet == null)
				return new BaseResponse<Breed>
				{
					Description = "Not found",
					StatusCode = StatusCode.NotFound
				};
			breedpet.Id = model.Id;
			breedpet.breedPetName = model.breedPetName;
			breedpet.PetTypeId = model.PetTypeId;

			await _breedRepository.Update(breedpet);


			return new BaseResponse<Breed>
			{
				Data = breedpet,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			return new BaseResponse<Breed>
			{
				Description = $"[EditBreedPet] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<IBaseResponse<bool>> Delete(Guid id)
	{
		try
		{
			var breedpet = await _breedRepository.GetByIdAsync(id);
			if (breedpet == null)
			{
				return new BaseResponse<bool>
				{
					Description = "Not found",
					StatusCode = StatusCode.NotFound,
					Data = false
				};
			}

			var servicetype = _serviceTypeRepository.GetAll().Result.FirstOrDefault(x => x.BreedId == id);
			if (servicetype != null) await _serviceTypeRepository.DeleteRange(servicetype);

			await _breedRepository.Delete(breedpet);


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
				Description = $"[DeleteBreedPet] : {ex.Message}",
				StatusCode = StatusCode.InternalServerError
			};
		}
	}
}