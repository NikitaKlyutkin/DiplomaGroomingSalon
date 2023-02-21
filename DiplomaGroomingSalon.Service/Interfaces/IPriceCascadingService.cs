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
		IBaseResponse<List<PetType>> GetTypePets();
		IBaseResponse<List<Breed>> GetBreedPets();
		IBaseResponse<List<ServiceType>> GetServiceTypes();
		Task<IBaseResponse<TypePetViewModel>> CreateTypePet(TypePetViewModel typePetViewModel);
		Task<IBaseResponse<BreedPetViewModel>> CreateBreedPet(BreedPetViewModel breedPetViewModel);
		Task<IBaseResponse<ServiceTypeViewModel>> CreateServiceType(ServiceTypeViewModel serviceTypeViewModel);
        Task<IBaseResponse<TypePetViewModel>> GetTypePet(Guid id);
        Task<IBaseResponse<BreedPetViewModel>> GetBreedPet(Guid id);
        Task<IBaseResponse<ServiceTypeViewModel>> GetServiceType(Guid id);
        Task<IBaseResponse<PetType>> EditTypePet(Guid id, TypePetViewModel model);
        Task<IBaseResponse<Breed>> EditBreedPet(Guid id, BreedPetViewModel model);
        Task<IBaseResponse<ServiceType>> EditServiceType(Guid id, ServiceTypeViewModel model);
        Task<IBaseResponse<bool>> DeleteTypePet(Guid id);
        Task<IBaseResponse<bool>> DeleteBreedPet(Guid id);
        Task<IBaseResponse<bool>> DeleteServiceType(Guid id);



	}
}
