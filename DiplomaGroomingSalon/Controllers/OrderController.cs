using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Implementations;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static MessagePack.MessagePackSerializer;

namespace DiplomaGroomingSalon.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly IAppointmentService _appointmentService;
        private readonly IPriceCascadingService _priceCascadingService;
        public OrderController(IOrderService orderService, IAppointmentService appointmentService, IPriceCascadingService priceCascadingService)
		{
			_orderService = orderService;
			_appointmentService = appointmentService;
			_priceCascadingService = priceCascadingService;

        }
        [Authorize]
        [HttpGet]
        public IActionResult CreateOrder()
        {
            var response = _appointmentService.GetAppointments();
            var appointment = response.Data;
            var responseTypePet = _priceCascadingService.GetTypePets();
            var typePets = responseTypePet.Data.ToList();
            
            ViewBag.DateTimeAppointment = new SelectList(appointment, "Id", "DateTimeAppointment");
			ViewBag.TypePetBPOrder = new SelectList(typePets, "IdTypePet", "typePetName");
			return View();
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateOrder(OrderViewModel model)
		{
			if (ModelState.IsValid)
			{
                var NameUser = User.Identity.Name;
                var ProfileUser = await _orderService.GetProfileOrder(NameUser);
                var ProfileId = ProfileUser.Data.Id;
                var response = await _orderService.CreateOrder(model, ProfileId);

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
