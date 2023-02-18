﻿using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DiplomaGroomingSalon.Controllers
{
	[Authorize]
	public class ProfileController : Controller
	{
		private readonly IProfileService _profileService;

		public ProfileController(IProfileService profileService)
		{
			_profileService = profileService;
		}

		[HttpPost]
		public async Task<IActionResult> Save(ProfileViewModel model)
		{
			ModelState.Remove("Id");
			ModelState.Remove("UserName");
			if (ModelState.IsValid)
			{
				var response = await _profileService.Save(model);
				if (response.StatusCode == Domain.Enum.StatusCode.OK)
				{
					return Json(new { description = response.Description });
				}
			}
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		public async Task<IActionResult> Detail()
		{
			var userName = User.Identity.Name;
			var response = await _profileService.GetProfile(userName);
            var res = response.Data.Id;
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return View();
		}
	}
}
