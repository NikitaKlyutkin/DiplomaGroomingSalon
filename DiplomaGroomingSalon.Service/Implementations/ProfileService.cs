using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace DiplomaGroomingSalon.Service.Implementations;

public class ProfileService : IProfileService
{
	private readonly ILogger<ProfileService> _logger;
	private readonly IAccountRepository<Profile> _profileRepository;

	public ProfileService(IAccountRepository<Profile> profileRepository,
		ILogger<ProfileService> logger)
	{
		_profileRepository = profileRepository;
		_logger = logger;
	}

	public async Task<BaseResponse<Profile>> GetProfile(string userName)
	{
		try
		{
			var profile = await _profileRepository.GetByNameAsync(userName);
			var profileView = new Profile
			{
				Id = profile!.Id,
				Name = profile.Name,
				Surname = profile.Surname,
				Phone = profile.Phone,
				Email = profile.Email,
				UserName = userName,
				UserId = profile.UserId
			};

			return new BaseResponse<Profile>
			{
				Data = profileView,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"[ProfileService.GetProfile] error: {ex.Message}");
			return new BaseResponse<Profile>
			{
				StatusCode = StatusCode.InternalServerError,
				Description = $"Internal error: {ex.Message}"
			};
		}
	}

	public async Task<BaseResponse<Profile>> Save(ProfileViewModel model)
	{
		try
		{
			var profile = await _profileRepository.GetByIdAsync(model.Id);

			profile!.Name = model.Name;
			profile.Surname = model.Surname;
			profile.Phone = model.Phone;
			profile.Email = model.Email;

			await _profileRepository.Update(profile);

			return new BaseResponse<Profile>
			{
				Data = profile,
				Description = "Data updated",
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"[ProfileService.Save] error: {ex.Message}");
			return new BaseResponse<Profile>
			{
				StatusCode = StatusCode.InternalServerError,
				Description = $"Internal error: {ex.Message}"
			};
		}
	}
}