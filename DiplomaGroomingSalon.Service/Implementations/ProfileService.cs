using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaGroomingSalon.Service.Implementations
{
	public class ProfileService : IProfileService
	{
		private readonly ILogger<ProfileService> _logger;
		private readonly IBaseRepository<Profile> _profileRepository;

		public ProfileService(IBaseRepository<Profile> profileRepository,
			ILogger<ProfileService> logger)
		{
			_profileRepository = profileRepository;
			_logger = logger;
		}

		public async Task<BaseResponse<ProfileViewModel>> GetProfile(string userName)
		{
			try
			{
				var profile = await _profileRepository.GetAll()
					.Select(x => new ProfileViewModel()
					{
						Id = x.Id,
						Name = x.Name,
						Surname = x.Surname,
						Phone = x.Phone,
						Email = x.Email,
						UserName = x.User.Name
					})
					.FirstOrDefaultAsync(x => x.UserName == userName);

				return new BaseResponse<ProfileViewModel>()
				{
					Data = profile,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"[ProfileService.GetProfile] error: {ex.Message}");
				return new BaseResponse<ProfileViewModel>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"Внутренняя ошибка: {ex.Message}"
				};
			}
		}

		public async Task<BaseResponse<Profile>> Save(ProfileViewModel model)
		{
			try
			{
				var profile = await _profileRepository.GetAll()
					.FirstOrDefaultAsync(x => x.Id == model.Id);

				profile.Name = model.Name;
				profile.Surname = model.Surname;
				profile.Phone = model.Phone;
				profile.Email = model.Email;

				await _profileRepository.Update(profile);

				return new BaseResponse<Profile>()
				{
					Data = profile,
					Description = "Данные обновлены",
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"[ProfileService.Save] error: {ex.Message}");
				return new BaseResponse<Profile>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"Внутренняя ошибка: {ex.Message}"
				};
			}
		}
	}
}
