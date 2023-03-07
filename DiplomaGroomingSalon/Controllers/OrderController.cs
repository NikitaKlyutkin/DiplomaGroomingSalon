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
using DiplomaGroomingSalon.Domain.Enum;
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
		private readonly ICRUDDataService<PetType> _petTypeService;

        public OrderController(IOrderService orderService, IAppointmentService appointmentService, ICRUDDataService<PetType> petTypeService)
		{
			_orderService = orderService;
			_appointmentService = appointmentService;
			_petTypeService = petTypeService;

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
		public async Task<IActionResult> GetOrdersByAdmin()
        {
	        var response = await _orderService.GetOrdersByAdmin();

	        if (response.StatusCode == Domain.Enum.StatusCode.OK)
	        {
		        return View(response.Data.ToList());
	        }
	        return View(response.Description);
        }
		[Authorize(Roles = "User")]
		[HttpGet]
		public async Task<IActionResult> GetOrdersByUser()
        {
	        var nameUser = User.Identity.Name;
	        var profileUser = await _orderService.GetProfileOrder(nameUser);
	        var profileId = profileUser.Data.Id;
			var response = await _orderService.GetOrdersByAdmin();
			ViewBag.ReportStates = new SelectList(Enum.GetNames(typeof(StatusOrder)));

			if (response.StatusCode == Domain.Enum.StatusCode.OK)
	        {
		        return View(response.Data.Where(x=>x.ProfileId == profileId));
	        }
	        return View(response.Description);
        }
		[Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var response = await _appointmentService.GetAppointments();
            var appointment = response.Data;
            var responseTypePet = await _petTypeService.GetAll();
            var typePets = responseTypePet.Data.ToList();
            
            ViewBag.DateTimeAppointment = new SelectList(appointment, "Id", "DateTimeAppointment");
			ViewBag.TypePetBPOrder = new SelectList(typePets, "Id", "PetTypeName");
			return View();
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateOrder(OrderViewModel model)
		{
			if (ModelState.IsValid)
			{
                var nameUser = User.Identity.Name;
                var profileUser = await _orderService.GetProfileOrder(nameUser);
                var profileId = profileUser.Data.Id;
                var response = await _orderService.CreateOrder(model, profileId);

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
