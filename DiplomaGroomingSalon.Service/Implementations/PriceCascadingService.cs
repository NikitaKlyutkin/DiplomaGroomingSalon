using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.DAL.Repositories;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace DiplomaGroomingSalon.Service.Implementations
{
	public class PriceCascadingService : IPriceCascadingService
	{
		private readonly IBaseRepository<PetType> _typePetRepository;
		private readonly IBaseRepository<Breed> _breedPetRepository;
		private readonly IBaseRepository<ServiceType> _serviceTypeRepository;
		public PriceCascadingService(IBaseRepository<PetType> typePetRepository, IBaseRepository<Breed> breedPetRepository, IBaseRepository<ServiceType> serviceTypeRepository)
		{
			_typePetRepository = typePetRepository;
			_breedPetRepository = breedPetRepository;
			_serviceTypeRepository = serviceTypeRepository;
			
		}
        public IBaseResponse<List<Breed>> GetBreedPets()
		{
			var baseResponse = new BaseResponse<IEnumerable<Breed>>();
			try
			{
				var breedPets = _breedPetRepository.GetAll().Include(x => x.PetType).ToList();
                if (!breedPets.Any())
				{
					return new BaseResponse<List<Breed>>()
					{
						Description = "Found 0 items",
						StatusCode = StatusCode.OK
					};
				}
				return new BaseResponse<List<Breed>>()
				{
					Data = breedPets,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Breed>>()
				{
					Description = $"[GetBreedPets] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}

		}
		public IBaseResponse<List<ServiceType>> GetServiceTypes()
		{
			var baseResponse = new BaseResponse<IEnumerable<ServiceType>>();
			try
			{
				var serviceTypes = _serviceTypeRepository.GetAll().Include(x => x.Breed.PetType).ToList();

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
					Data = serviceTypes,
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
		public async Task<IBaseResponse<TypePetViewModel>> CreateTypePet(TypePetViewModel typePetViewModel)
		{
			var baseResponse = new BaseResponse<TypePetViewModel>();
			try
			{
				var typePet = new PetType()
				{
					Id = Guid.NewGuid(),
					typePetName = typePetViewModel.typePetName
				};

				await _typePetRepository.Create(typePet);

			}
			catch (Exception ex)
			{
				return new BaseResponse<TypePetViewModel>()
				{
					Description = $"[CreateTypePet]: {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<BreedPetViewModel>> CreateBreedPet(BreedPetViewModel breedPetViewModel)
		{
			var baseResponse = new BaseResponse<BreedPetViewModel>();
			try
			{
				var breedPet = new Breed()
				{
					Id = new Guid(),
					TypePetId = breedPetViewModel.TypePetId,
					breedPetName = breedPetViewModel.breedPetName
				};

				await _breedPetRepository.Create(breedPet);

			}
			catch (Exception ex)
			{
				return new BaseResponse<BreedPetViewModel>()
				{
					Description = $"[CreateBreedPet]: {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

		public async Task<IBaseResponse<ServiceTypeViewModel>> CreateServiceType(ServiceTypeViewModel serviceTypeViewModel)
		{
			var baseResponse = new BaseResponse<ServiceTypeViewModel>();
			try
			{
				var serviceType = new ServiceType()
				{
					Id = new Guid(),
					BreedPetId = serviceTypeViewModel.BreedPetId,
					TypePetId = serviceTypeViewModel.TypePetId,
					Price = serviceTypeViewModel.Price,
					serviceTypeName = serviceTypeViewModel.serviceTypeName
					
				};

				await _serviceTypeRepository.Create(serviceType);

			}
			catch (Exception ex)
			{
				return new BaseResponse<ServiceTypeViewModel>()
				{
					Description = $"[CreateServiceType]: {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}

        public async Task<IBaseResponse<TypePetViewModel>> GetTypePet(Guid id)
        {
            try
            {
                var typepet = await _typePetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (typepet == null)
                {
                    return new BaseResponse<TypePetViewModel>()
                    {
                        Description = "Not Found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new TypePetViewModel()
                {
					Id = typepet.Id,
					typePetName = typepet.typePetName,
					//TypePetId = typepet.TypePetId

                };

                return new BaseResponse<TypePetViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<TypePetViewModel>()
                {
                    Description = $"[GetTypePet] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
				};
			}
		}
        public async Task<IBaseResponse<BreedPetViewModel>> GetBreedPet(Guid id)
        {
            try
            {
                var breedpet = await _breedPetRepository.GetAll().Include(x=>x.PetType).FirstOrDefaultAsync(x => x.Id == id);
                if (breedpet == null)
                {
                    return new BaseResponse<BreedPetViewModel>()
                    {
                        Description = "Not Found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var data = new BreedPetViewModel()
                {
                 
                    Id = breedpet.Id,
					breedPetName = breedpet.breedPetName,
					TypePetId = breedpet.TypePetId,
                    TypePetName = breedpet.PetType.typePetName
                    

                };

                return new BaseResponse<BreedPetViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<BreedPetViewModel>()
                {
                    Description = $"[GetBreedPet] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<ServiceTypeViewModel>> GetServiceType(Guid id)
        {
            try
            {
                var servicetype = await _serviceTypeRepository.GetAll().Include(x=>x.Breed.PetType).FirstOrDefaultAsync(x => x.Id == id);
                if (servicetype == null)
                {
                    return new BaseResponse<ServiceTypeViewModel>()
                    {
                        Description = "Not Found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var data = new ServiceTypeViewModel()
                {
                   Id = servicetype.Id,
				   serviceTypeName = servicetype.serviceTypeName,
                   TypePetName = servicetype.Breed.PetType.typePetName,
                   BreedPetName = servicetype.Breed.breedPetName,
				   TypePetId = servicetype.TypePetId,
				   BreedPetId = servicetype.BreedPetId,
				   Price = servicetype.Price

                };

                return new BaseResponse<ServiceTypeViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ServiceTypeViewModel>()
                {
                    Description = $"[GetBreedPet] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<PetType>> EditTypePet(Guid id, TypePetViewModel model)
        {
            try
            {
                var typepet = await _typePetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (typepet == null)
				{
                    return new BaseResponse<PetType>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                typepet.Id=model.Id;
				typepet.typePetName=model.typePetName;
                //typepet.TypePetId = model.TypePetId;

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
        public async Task<IBaseResponse<Breed>> EditBreedPet(Guid id, BreedPetViewModel model)
        {
            try
            {
                var breedpet = await _breedPetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (breedpet == null)
                {
                    return new BaseResponse<Breed>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                breedpet.Id = model.Id;
				breedpet.breedPetName = model.breedPetName;
				breedpet.TypePetId = model.TypePetId;

                await _breedPetRepository.Update(breedpet);


                return new BaseResponse<Breed>()
                {
                    Data = breedpet,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Breed>()
                {
                    Description = $"[EditBreedPet] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<ServiceType>> EditServiceType(Guid id, ServiceTypeViewModel model)
        {
            try
            {
                var servicetype = await _serviceTypeRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (servicetype == null)
                {
                    return new BaseResponse<ServiceType>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                servicetype.Id = model.Id;
                servicetype.TypePetId = model.TypePetId;
				servicetype.BreedPetId = model.BreedPetId;
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
        public async Task<IBaseResponse<bool>> DeleteTypePet(Guid id)
        {
            try
            {
                var typepet = await _typePetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
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
                    var breedpet = await _breedPetRepository.GetAll().FirstOrDefaultAsync(x => x.TypePetId == id);
                    var servicetype = await _serviceTypeRepository.GetAll().FirstOrDefaultAsync(x => x.TypePetId == id);
                    if (servicetype != null)
                    {
                        await _serviceTypeRepository.DeleteRange(servicetype!);
                    }

                    if (breedpet != null)
                    {
                        await _breedPetRepository.DeleteRange(breedpet!);
                    }

                    await _typePetRepository.Delete(typepet);
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
        public async Task<IBaseResponse<bool>> DeleteBreedPet(Guid id)
        {
            try
            {
                var breedpet = await _breedPetRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (breedpet == null)
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
                    var servicetype = await _serviceTypeRepository.GetAll().FirstOrDefaultAsync(x => x.BreedPetId == id);
                    if (servicetype != null)
                    {
                        await _serviceTypeRepository.DeleteRange(servicetype!);
                    }

                    await _breedPetRepository.Delete(breedpet);
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
                    Description = $"[DeleteBreedPet] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<bool>> DeleteServiceType(Guid id)
        {
	        try
	        {
		        var serviceType = await _serviceTypeRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
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
