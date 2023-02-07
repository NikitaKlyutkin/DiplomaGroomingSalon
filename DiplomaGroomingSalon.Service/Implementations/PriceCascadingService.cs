using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
		private readonly IBaseRepository<TypePet> _typePetRepository;
		private readonly IBaseRepository<BreedPet> _breedPetRepository;
		private readonly IBaseRepository<ServiceType> _serviceTypeRepository;
		public PriceCascadingService(IBaseRepository<TypePet> typePetRepository, IBaseRepository<BreedPet> breedPetRepository, IBaseRepository<ServiceType> serviceTypeRepository)
		{
			_typePetRepository = typePetRepository;
			_breedPetRepository = breedPetRepository;
			_serviceTypeRepository = serviceTypeRepository;
			
		}

		public IBaseResponse<List<TypePet>> GetTypePets()
		{
			var baseResponse = new BaseResponse<IEnumerable<TypePet>>();
			try
			{
				var typePets = _typePetRepository.GetAll().ToList();

				if (!typePets.Any())
				{
					return new BaseResponse<List<TypePet>>()
					{
						Description = "Found 0 items",
						StatusCode = StatusCode.OK
					};
				}
				return new BaseResponse<List<TypePet>>()
				{
					Data = typePets,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<TypePet>>()
				{
					Description = $"[GetTypePets] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}

		}
		public IBaseResponse<List<BreedPet>> GetBreedPets()
		{
			var baseResponse = new BaseResponse<IEnumerable<BreedPet>>();
			try
			{
				var breedPets = _breedPetRepository.GetAll().ToList();

				if (!breedPets.Any())
				{
					return new BaseResponse<List<BreedPet>>()
					{
						Description = "Found 0 items",
						StatusCode = StatusCode.OK
					};
				}
				return new BaseResponse<List<BreedPet>>()
				{
					Data = breedPets,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<BreedPet>>()
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
				var serviceTypes = _serviceTypeRepository.GetAll().ToList();

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
				var typePet = new TypePet()
				{
					IdTypePet = Guid.NewGuid(),
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
				var breedPet = new BreedPet()
				{
					IdBreedPet = new Guid(),
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
					IdServiceType = new Guid(),
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
                var typepet = await _typePetRepository.GetAll().FirstOrDefaultAsync(x => x.IdTypePet == id);
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
					IdTypePet = typepet.IdTypePet,
					typePetName = typepet.typePetName,
					TypePetId = typepet.TypePetId

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
                var breedpet = await _breedPetRepository.GetAll().FirstOrDefaultAsync(x => x.IdBreedPet == id);
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
                    IdBreedPet = breedpet.IdBreedPet,
					breedPetName = breedpet.breedPetName,
					TypePetId = breedpet.TypePetId

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
                var servicetype = await _serviceTypeRepository.GetAll().FirstOrDefaultAsync(x => x.IdServiceType == id);
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
                   IdServiceType = servicetype.IdServiceType,
				   serviceTypeName = servicetype.serviceTypeName,
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
        public async Task<IBaseResponse<TypePet>> EditTypePet(Guid id, TypePetViewModel model)
        {
            try
            {
                var typepet = await _typePetRepository.GetAll().FirstOrDefaultAsync(x => x.IdTypePet == id);
                if (typepet == null)
				{
                    return new BaseResponse<TypePet>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                typepet.IdTypePet=model.IdTypePet;
				typepet.typePetName=model.typePetName;
                typepet.TypePetId = model.TypePetId;

                await _typePetRepository.Update(typepet);


                return new BaseResponse<TypePet>()
                {
                    Data = typepet,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
			{
                return new BaseResponse<TypePet>()
                {
                    Description = $"[EditTypePet] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<BreedPet>> EditBreedPet(Guid id, BreedPetViewModel model)
        {
            try
            {
                var breedpet = await _breedPetRepository.GetAll().FirstOrDefaultAsync(x => x.IdBreedPet == id);
                if (breedpet == null)
                {
                    return new BaseResponse<BreedPet>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                breedpet.IdBreedPet = model.IdBreedPet;
				breedpet.breedPetName = model.breedPetName;
				breedpet.TypePetId = model.TypePetId;

                await _breedPetRepository.Update(breedpet);


                return new BaseResponse<BreedPet>()
                {
                    Data = breedpet,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<BreedPet>()
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
                var servicetype = await _serviceTypeRepository.GetAll().FirstOrDefaultAsync(x => x.IdServiceType == id);
                if (servicetype == null)
                {
                    return new BaseResponse<ServiceType>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                servicetype.IdServiceType = model.IdServiceType;
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
                var typepet = await _typePetRepository.GetAll().FirstOrDefaultAsync(x => x.IdTypePet == id);
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
                    await _serviceTypeRepository.DeleteRange(servicetype!);
                    await _breedPetRepository.DeleteRange(breedpet!);
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
                var breedpet = await _breedPetRepository.GetAll().FirstOrDefaultAsync(x => x.IdBreedPet == id);
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
                    await _serviceTypeRepository.DeleteRange(servicetype!);
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
		        var serviceType = await _serviceTypeRepository.GetAll().FirstOrDefaultAsync(x => x.IdServiceType == id);
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
