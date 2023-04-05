using System.Security.Claims;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;

namespace DiplomaGroomingSalon.Service.Interfaces;

public interface IAccountService
{
	Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

	Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

	Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
}