using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Implementations;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using static MessagePack.MessagePackSerializer;

namespace DiplomaGroomingSalon.Controllers
{
    public class AppointmentController : Controller
	{
		private readonly IAppointmentService _appointmentService;
		private readonly IProfileService _profileService;

		public AppointmentController(IAppointmentService appointmentService, IProfileService profileService)
		{
			_appointmentService = appointmentService;
			_profileService = profileService;
		}
        [Authorize(Roles = "Admin")]
        [HttpGet]
		public IActionResult CreateAppointment()
		{
			return View();
		}
        [Authorize(Roles = "Admin")]
        [HttpPost]
		public async Task<IActionResult> CreateAppointment(AppointmentViewModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _appointmentService.CreateAppointment(model);
				if (response.StatusCode == Domain.Enum.StatusCode.OK)
				{
					return Json(new { description = response.Description });
				}
				return RedirectToAction("CreateAppointment");

			}
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet]
		public async Task<IActionResult> GetAppointments()
		{
			var response = await _appointmentService.GetAppointments();
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return View("Error", $"{response.Description}");
		}
	}
}
