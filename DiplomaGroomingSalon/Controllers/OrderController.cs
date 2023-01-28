using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Implementations;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Response;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DiplomaGroomingSalon.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly IAppointmentService _appointmentService;
		public OrderController(IOrderService orderService, IAppointmentService appointmentService)
		{
			_orderService = orderService;
			_appointmentService = appointmentService;
		}
		[HttpGet]
		public IActionResult CreateOrder()
        {
            var response = _appointmentService.GetAppointments();
            var appointment = response.Data;
            ViewBag.DateTimeAppointment = new SelectList(appointment, "Id", "DateTimeAppointment");
	        return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder(OrderViewModel model)
		{
			if (ModelState.IsValid)
			{
				
				var response = await _orderService.CreateOrder(model);

				if (response.StatusCode == Domain.Enum.StatusCode.OK)
				{
					return Json(new { description = response.Description });
				}
				return RedirectToAction("CreateOrder");

			}
			return StatusCode(StatusCodes.Status500InternalServerError);
		}
	}
}
