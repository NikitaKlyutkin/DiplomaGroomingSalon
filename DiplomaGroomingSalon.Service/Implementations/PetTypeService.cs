using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaGroomingSalon.Service.Implementations
{
    public class PetTypeService : ICRUDDataService<PetType>
    {
        private readonly IBaseRepository<PetType> _typePetRepository;
        private readonly IBaseRepository<Breed> _breedPetRepository;
        private readonly IBaseRepository<ServiceType> _serviceTypeRepository;
        public PetTypeService(IBaseRepository<PetType> typePetRepository, IBaseRepository<Breed> breedPetRepository, IBaseRepository<ServiceType> serviceTypeRepository)
        {
            _typePetRepository = typePetRepository;
            _breedPetRepository = breedPetRepository;
            _serviceTypeRepository = serviceTypeRepository;

        }
        public async Task<IBaseResponse<List<PetType>>> GetAll()
        {
            var baseResponse = new BaseResponse<IEnumerable<PetType>>();
            try
            {
                var typePets = await _typePetRepository.GetAll();

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
                var typepet = await _typePetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
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

                await _typePetRepository.Create(typePet);

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
                var typepet = _typePetRepository.GetAll().Result.FirstOrDefault(x => x.Id == id);
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

                await _typePetRepository.Update(typepet);


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

        public Task<IBaseResponse<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
