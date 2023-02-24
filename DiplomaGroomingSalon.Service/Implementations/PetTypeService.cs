using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Service.Interfaces;

namespace DiplomaGroomingSalon.Service.Implementations
{
    public class PetTypeService : ICRUDDataService<PetType>
    {
        private readonly IBaseRepository<PetType> _petTypePetRepository;
        private readonly IBaseRepository<Breed> _breedRepository;
        private readonly IBaseRepository<ServiceType> _serviceTypeRepository;
        public PetTypeService(IBaseRepository<PetType> petTypePetRepository, IBaseRepository<Breed> breedRepository, IBaseRepository<ServiceType> serviceTypeRepository)
        {
            _petTypePetRepository = petTypePetRepository;
            _breedRepository = breedRepository;
            _serviceTypeRepository = serviceTypeRepository;

        }
        public async Task<IBaseResponse<List<PetType>>> GetAll()
        {
            var baseResponse = new BaseResponse<IEnumerable<PetType>>();
            try
            {
                var typePets = await _petTypePetRepository.GetAll();

                if (!typePets.Any())
                {
                    return new BaseResponse<List<PetType>>()
                    {
                        Description = "Found 0 items",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<PetType>>()
                {
                    Data = typePets.ToList(),
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<PetType>>()
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
                var typepet = await _petTypePetRepository.GetByIdAsync(id);
                if (typepet == null)
                {
                    return new BaseResponse<PetType>()
                    {
                        Description = "Not Found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new PetType()
                {
                    Id = typepet.Id,
                    typePetName = typepet.typePetName,

                };

                return new BaseResponse<PetType>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PetType>()
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
                var typePet = new PetType()
                {
                    Id = Guid.NewGuid(),
                    typePetName = model.typePetName
                };

                await _petTypePetRepository.Create(typePet);

            }
            catch (Exception ex)
            {
                return new BaseResponse<PetType>()
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
                var typepet = await _petTypePetRepository.GetByIdAsync(id);
                if (typepet == null)
                {
                    return new BaseResponse<PetType>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                typepet.Id = model.Id;
                typepet.typePetName = model.typePetName;

                await _petTypePetRepository.Update(typepet);


                return new BaseResponse<PetType>()
                {
                    Data = typepet,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PetType>()
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
                var typepet = await _petTypePetRepository.GetByIdAsync(id);
                if (typepet == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound,
                        Data = false
                    };
                }
                else
                {
                  var breedpet = _breedRepository.GetAll().Result.FirstOrDefault(x => x.PetTypeId == id);
                  var servicetype = _serviceTypeRepository.GetAll().Result.FirstOrDefault(x => x.PetTypeId == id);
                    if (servicetype != null)
                    {
                        await _serviceTypeRepository.DeleteRange(servicetype);
                    }

                    if (breedpet != null)
                    {
                        await _breedRepository.DeleteRange(breedpet);
                    }

                    await _petTypePetRepository.Delete(typepet);
                }


                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteTypePet] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
