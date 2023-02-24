using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.DAL.Repositories;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;

namespace DiplomaGroomingSalon.Service.Implementations
{
    public class ServiceTypeService : ICRUDDataService<ServiceType>
    {
        private readonly IBaseRepository<PetType> _petTypePetRepository;
        private readonly IBaseRepository<Breed> _breedRepository;
        private readonly IBaseRepository<ServiceType> _serviceTypeRepository;
        public ServiceTypeService(IBaseRepository<PetType> petTypePetRepository, IBaseRepository<Breed> breedRepository, IBaseRepository<ServiceType> serviceTypeRepository)
        {
            _petTypePetRepository = petTypePetRepository;
            _breedRepository = breedRepository;
            _serviceTypeRepository = serviceTypeRepository;

        }
        public async Task<IBaseResponse<List<ServiceType>>> GetAll()
        {
            var baseResponse = new BaseResponse<IEnumerable<ServiceType>>();
            try
            {
                var serviceTypes = await _serviceTypeRepository.GetAll();

                if (!serviceTypes.Any())
                {
                    return new BaseResponse<List<ServiceType>>()
                    {
                        Description = "Found 0 items",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<ServiceType>>()
                {
                    Data = serviceTypes.ToList(),
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<ServiceType>>()
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
                var servicetype = await _serviceTypeRepository.GetByIdAsync(id);
                if (servicetype == null)
                {
                    return new BaseResponse<ServiceType>()
                    {
                        Description = "Not Found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var data = new ServiceType()
                {
                    Id = servicetype.Id,
                    serviceTypeName = servicetype.serviceTypeName,
                    TypePetName = servicetype.Breed.PetType.typePetName,
                    BreedPetName = servicetype.Breed.breedPetName,
                    PetTypeId = servicetype.PetTypeId,
                    BreedId = servicetype.BreedId,
                    Price = servicetype.Price

                };

                return new BaseResponse<ServiceType>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ServiceType>()
                {
                    Description = $"[GetBreedPet] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ServiceType>> Create(ServiceType model)
        {
            var baseResponse = new BaseResponse<ServiceType>();
            try
            {
                var serviceType = new ServiceType()
                {
                    Id = new Guid(),
                    BreedId = model.BreedId,
                    PetTypeId = model.PetTypeId,
                    Price = model.Price,
                    serviceTypeName = model.serviceTypeName

                };

                await _serviceTypeRepository.Create(serviceType);

            }
            catch (Exception ex)
            {
                return new BaseResponse<ServiceType>()
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
                var servicetype = await _serviceTypeRepository.GetByIdAsync(id);
                if (servicetype == null)
                {
                    return new BaseResponse<ServiceType>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                servicetype.Id = model.Id;
                servicetype.PetTypeId = model.PetTypeId;
                servicetype.BreedId = model.BreedId;
                servicetype.serviceTypeName = model.serviceTypeName;
                servicetype.Price = model.Price;

                await _serviceTypeRepository.Update(servicetype);


                return new BaseResponse<ServiceType>()
                {
                    Data = servicetype,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ServiceType>()
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
                var serviceType = await _serviceTypeRepository.GetByIdAsync(id);
                if (serviceType == null)
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
                    await _serviceTypeRepository.Delete(serviceType);
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
                    Description = $"[DeleteServiceType] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
