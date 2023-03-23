using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;

namespace DiplomaGroomingSalon.Service.Interfaces
{
	public interface IProfileService
	{
		Task<BaseResponse<Profile>> GetProfile(string userName);

		Task<BaseResponse<Profile>> Save(ProfileViewModel model);
	}
}
