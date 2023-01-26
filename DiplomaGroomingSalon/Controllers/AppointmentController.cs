using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaGroomingSalon.Controllers
{
	public class AppointmentController : Controller
	{
		private readonly IAppointmentService _appointmentService;

		public AppointmentController(IAppointmentService appointmentService)
		{
			_appointmentService = appointmentService;
		}

		[HttpGet]
		public IActionResult CreateAppointment()
		{
			var appointmentViewModel = new AppointmentViewModel();
			return View(appointmentViewModel);
		}

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
		public IActionResult GetAppointments()
		{
			var response = _appointmentService.GetAppointments();
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return View("Error", $"{response.Description}");
		}
	}
}
