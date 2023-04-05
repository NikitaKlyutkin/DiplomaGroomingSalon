using System.Security.Claims;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Helpers;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace DiplomaGroomingSalon.Service.Implementations;

public class AccountService : IAccountService
{
	private readonly ILogger<AccountService> _logger;
	private readonly IAccountRepository<Profile> _proFileRepository;
	private readonly IAccountRepository<User> _userRepository;

	public AccountService(IAccountRepository<User> userRepository,
		ILogger<AccountService> logger, IAccountRepository<Profile> proFileRepository)
	{
		_userRepository = userRepository;
		_logger = logger;
		_proFileRepository = proFileRepository;
	}

	public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
	{
		try
		{
			var user = await _userRepository.GetByNameAsync(model.Name);
			if (user != null)
				return new BaseResponse<ClaimsIdentity>
				{
					Description = "There is already a user with this login"
				};

			user = new User
			{
				Name = model.Name,
				Role = Role.User,
				Password = HashPasswordHelper.HashPassowrd(model.Password)
			};

			await _userRepository.Create(user);

			var profile = new Profile
			{
				UserId = user.Id
			};

			await _proFileRepository.Create(profile);
			var result = Authenticate(user);

			return new BaseResponse<ClaimsIdentity>
			{
				Data = result,
				Description = "Объект добавился",
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"[Register]: {ex.Message}");
			return new BaseResponse<ClaimsIdentity>
			{
				Description = ex.Message,
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
	{
		try
		{
			var user = await _userRepository.GetByNameAsync(model.Name);
			if (user == null)
				return new BaseResponse<ClaimsIdentity>
				{
					Description = "User is not found"
				};

			if (user.Password != HashPasswordHelper.HashPassowrd(model.Password))
				return new BaseResponse<ClaimsIdentity>
				{
					Description = "Invalid password or login"
				};
			var result = Authenticate(user);

			return new BaseResponse<ClaimsIdentity>
			{
				Data = result,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"[Login]: {ex.Message}");
			return new BaseResponse<ClaimsIdentity>
			{
				Description = ex.Message,
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
	{
		try
		{
			var user = await _userRepository.GetByNameAsync(model.UserName);
			if (user == null)
				return new BaseResponse<bool>
				{
					StatusCode = StatusCode.UserNotFound,
					Description = "User is not found"
				};

			user.Password = HashPasswordHelper.HashPassowrd(model.NewPassword);
			await _userRepository.Update(user);

			return new BaseResponse<bool>
			{
				Data = true,
				StatusCode = StatusCode.OK,
				Description = "Password updated"
			};
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
			return new BaseResponse<bool>
			{
				Description = ex.Message,
				StatusCode = StatusCode.InternalServerError
			};
		}
	}

	private ClaimsIdentity Authenticate(User user)
	{
		var claims = new List<Claim>
		{
			new(ClaimsIdentity.DefaultNameClaimType, user.Name),
			new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
		};
		return new ClaimsIdentity(claims, "ApplicationCookie",
			ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
	}
}