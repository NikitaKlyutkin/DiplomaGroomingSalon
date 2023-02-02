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
		

        public IBaseResponse<List<BreedPet>> GetSortBreedPet(Guid TypePetId)
        {
            var baseResponse = new BaseResponse<IEnumerable<BreedPet>>();
            try
            {
                var breedPets = _breedPetRepository.GetAll().ToList();
                var sortBreedPet = new List<BreedPet>();
                if (!breedPets.Any())
                {
                    return new BaseResponse<List<BreedPet>>()
                    {
                        Description = "Found 0 items",
                        StatusCode = StatusCode.OK
                    };
                }

                foreach (var breedPet in breedPets)
                {
                    if (breedPet.TypePetId == TypePetId)
                    {
                        sortBreedPet.Add(breedPet);
                    }
                }

                return new BaseResponse<List<BreedPet>>()
                {
                    Data = sortBreedPet,
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
	}
}
