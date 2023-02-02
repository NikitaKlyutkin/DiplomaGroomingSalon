using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Service.Interfaces
{
	public interface IPriceCascadingService
	{
		IBaseResponse<List<TypePet>> GetTypePets();
		IBaseResponse<List<BreedPet>> GetBreedPets();
		IBaseResponse<List<ServiceType>> GetServiceTypes();
		IBaseResponse<List<BreedPet>> GetSortBreedPet(Guid TypePetId);
        Task<IBaseResponse<TypePetViewModel>> CreateTypePet(TypePetViewModel typePetViewModel);
		Task<IBaseResponse<BreedPetViewModel>> CreateBreedPet(BreedPetViewModel breedPetViewModel);
		Task<IBaseResponse<ServiceTypeViewModel>> CreateServiceType(ServiceTypeViewModel serviceTypeViewModel);

	}
}
