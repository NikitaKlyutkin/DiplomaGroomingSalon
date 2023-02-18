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
	public interface IProfileService
	{
		Task<BaseResponse<ProfileViewModel>> GetProfile(string userName);

		Task<BaseResponse<Profile>> Save(ProfileViewModel model);
	}
}
