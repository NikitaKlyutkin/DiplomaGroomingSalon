using System;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaGroomingSalon.Controllers;

public class AppointmentController : Controller
{
	private readonly IAppointmentService _appointmentService;

	public AppointmentController(IAppointmentService appointmentService)
	{
		_appointmentService = appointmentService;
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
			if (model.DateTimeAppointment <= DateTime.Now)
			{
				ModelState.AddModelError("DateTimeAppointment", "The appointment date and time must be in the future.");
				return View(model);
			}
			var response = await _appointmentService.CreateAppointment(model);
			if (response.StatusCode == Domain.Enum.StatusCode.OK) return Json(new {description = response.Description});
			return RedirectToAction("CreateAppointment");
		}

		return StatusCode(StatusCodes.Status500InternalServerError);
	}

	[HttpGet]
	public async Task<IActionResult> GetAppointments()
	{
		var response = await _appointmentService.GetAppointmentsFree();
		if (response.StatusCode == Domain.Enum.StatusCode.OK) return View(response.Data);
		return View("Error", $"{response.Description}");
	}

	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> DeleteAppointment(Guid id)
	{
		var response = await _appointmentService.Delete(id);
		if (response.StatusCode == Domain.Enum.StatusCode.OK) return RedirectToAction("GetAppointments");
		return View(response.Description);
	}
}