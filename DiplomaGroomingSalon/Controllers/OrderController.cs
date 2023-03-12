using System;
using System.Linq;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using DiplomaGroomingSalon.Domain.Enum;

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
		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> GetActualOrders()
		{
			var responseAsync = await _orderService.GetOrdersByAdmin();
			var response = responseAsync.Data.Where(x => x.StatusOrder == StatusOrder.During);

			if (responseAsync.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.ToList());
			}
			return View(responseAsync.Description);
		}
		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> GetCompletedOrders()
		{
			var responseAsync = await _orderService.GetOrdersByAdmin();
			var response = responseAsync.Data.Where(x => x.StatusOrder == StatusOrder.Сompleted);

			if (responseAsync.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.ToList());
			}
			return View(responseAsync.Description);
		}
		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> GetCancellationsOrders()
		{
			var responseAsync = await _orderService.GetOrdersByAdmin();
			var response = responseAsync.Data
				.Where(x => x.StatusOrder == StatusOrder.Cancellations || x.StatusOrder == StatusOrder.NotDone);

			if (responseAsync.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.ToList());
			}
			return View(responseAsync.Description);
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
            var response = await _appointmentService.GetAppointmentsFree();
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
		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> DetailOrder(Guid id)
		{
			var response = await _orderService.GetById(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			ModelState.AddModelError("", response.Description);
			return View();
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> DetailOrder(Order viewModel)
		{
			if (ModelState.IsValid)
			{
				if (viewModel.StatusOrder == StatusOrder.Cancellations && viewModel.Appointment.DateTimeAppointment > DateTime.Now.AddHours(2))
				{
					var appointmentAsync = await _appointmentService.GetById(viewModel.AppointmentId);
					var appointmentData = appointmentAsync.Data;
					var appointment = new Appointment()
					{
						Id = appointmentData!.Id,
						DateTimeAppointment = appointmentData.DateTimeAppointment,
						StatusAppointment = true,
						Description = appointmentData.Description
					};
					await _appointmentService.UpdateAppointment(appointment.Id, appointment);
				}
				else
				{
					var appointmentAsync = await _appointmentService.GetById(viewModel.AppointmentId);
					var appointmentData = appointmentAsync.Data;
					var appointment = new Appointment()
					{
						Id = appointmentData!.Id,
						DateTimeAppointment = appointmentData.DateTimeAppointment,
						StatusAppointment = false,
						Description = appointmentData.Description
					};
					await _appointmentService.UpdateAppointment(appointment.Id, appointment);
				}
				await _orderService.Edit(viewModel.Id, viewModel);
			}

			return RedirectToAction("GetOrdersByAdmin");
		}
		[Authorize(Roles = "User")]
		public async Task<IActionResult> CancellationsOrder(Guid id, Order viewModel)
		{
			if (ModelState.IsValid)
			{
				var getOrderById = await _orderService.GetById(id);
				var orderAsync = getOrderById.Data;
				
				viewModel.NamePet = orderAsync.NamePet;
				viewModel.TypePetId = orderAsync.TypePetId;
				viewModel.BreedPetId = orderAsync.BreedPetId;
				viewModel.ServiceTypeId = orderAsync.ServiceTypeId;
				viewModel.ProfileId = orderAsync.ProfileId;
				viewModel.AppointmentId = orderAsync.AppointmentId;
				viewModel.Price = orderAsync.Price;
				viewModel.StatusOrder = StatusOrder.Cancellations;
				viewModel.Description = orderAsync.Description;

				var getAppointment = await _appointmentService.GetById(orderAsync.AppointmentId);
				var appointmentAsync = getAppointment.Data;
				if (appointmentAsync.DateTimeAppointment > DateTime.Now.AddHours(2))
				{
					var appointment = new Appointment()
					{
						Id = appointmentAsync.Id,
						DateTimeAppointment = appointmentAsync.DateTimeAppointment,
						StatusAppointment = true,
						Description = appointmentAsync.Description
					};
					await _appointmentService.UpdateAppointment(appointment.Id, appointment);
				}
				await _orderService.Edit(id, viewModel);
			}
			return RedirectToAction("GetOrdersByUser");
		}

		public async Task<IActionResult> RestoringOrder(Guid id, Order viewModel)
		{
			if (ModelState.IsValid)
			{
				var getOrderById = await _orderService.GetById(id);
				var orderAsync = getOrderById.Data;

				viewModel.NamePet = orderAsync.NamePet;
				viewModel.TypePetId = orderAsync.TypePetId;
				viewModel.BreedPetId = orderAsync.BreedPetId;
				viewModel.ServiceTypeId = orderAsync.ServiceTypeId;
				viewModel.ProfileId = orderAsync.ProfileId;
				viewModel.AppointmentId = orderAsync.AppointmentId;
				viewModel.Price = orderAsync.Price;
				viewModel.StatusOrder = StatusOrder.During;
				viewModel.Description = orderAsync.Description;

				var getAppointment = await _appointmentService.GetById(orderAsync.AppointmentId);
				var appointmentAsync = getAppointment.Data;

				if (appointmentAsync.StatusAppointment == true && appointmentAsync.DateTimeAppointment > DateTime.Now.AddHours(2))
				{
					var appointment = new Appointment()
					{
						Id = appointmentAsync.Id,
						DateTimeAppointment = appointmentAsync.DateTimeAppointment,
						StatusAppointment = false,
						Description = appointmentAsync.Description
					};
					await _appointmentService.UpdateAppointment(appointment.Id, appointment);
					await _orderService.Edit(id, viewModel);
				}
			}
			return RedirectToAction("GetOrdersByUser");
		}

	}
}
