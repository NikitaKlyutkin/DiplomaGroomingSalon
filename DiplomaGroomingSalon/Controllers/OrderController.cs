using System;
using System.Linq;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomaGroomingSalon.Controllers;

public class OrderController : Controller
{
	private readonly IAppointmentService _appointmentService;
	private readonly IOrderService _orderService;
	private readonly ICRUDDataService<PetType> _petTypeService;

	public OrderController(IOrderService orderService, IAppointmentService appointmentService,
		ICRUDDataService<PetType> petTypeService)
	{
		_orderService = orderService;
		_appointmentService = appointmentService;
		_petTypeService = petTypeService;
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IActionResult> GetOrders(string type = "all")
	{
		var responseAsync = await _orderService.GetOrders();
		var orders = responseAsync.Data;

		switch (type)
		{
			case "actual":
				orders = orders.Where(x => x.StatusOrder == StatusOrder.During).ToList();
				ViewData["Title"] = "Actual Orders";
				break;
			case "completed":
				orders = orders.Where(x => x.StatusOrder == StatusOrder.Сompleted).ToList();
				ViewData["Title"] = "Completed Orders";
				break;
			case "cancellations":
				orders = orders.Where(x => x.StatusOrder == StatusOrder.Cancellations).ToList();
				ViewData["Title"] = "Cancellations Orders";
				break;
			default:
				ViewData["Title"] = "All Orders";
				break;
		}

		return View(orders.ToList());
	}

	[Authorize(Roles = "User")]
	[HttpGet]
	public async Task<IActionResult> GetOrdersByUser()
	{
		var nameUser = User.Identity.Name;
		var profileUser = await _orderService.GetProfileOrder(nameUser);
		var profileId = profileUser.Data.Id;
		var response = await _orderService.GetOrders();
		ViewBag.ReportStates = new SelectList(Enum.GetNames(typeof(StatusOrder)));

		if (response.Data != null) return View(response.Data.Where(x => x.ProfileId == profileId));
		return View();
	}

	[Authorize(Roles = "User")]
	[HttpGet]
	public async Task<IActionResult> CreateOrder()
	{
		var response = await _appointmentService.GetAppointmentsFree();
		var appointment = response.Data;
		var responseTypePet = await _petTypeService.GetAll();
		var typePets = responseTypePet.Data;
		if (appointment != null)
			ViewBag.DateTimeAppointment = new SelectList(appointment, "Id", "DateTimeAppointment");
		else
			ViewBag.DateTimeAppointment = null;

		if (typePets != null)
			ViewBag.TypePetBPOrder = new SelectList(typePets, "Id", "PetTypeName");
		else
			ViewBag.TypePetBPOrder = null;

		var nameUser = User.Identity.Name;
		var profileUser = await _orderService.GetProfileOrder(nameUser);
		if (profileUser.Data.Phone == null || profileUser.Data.Name == null)
			ViewBag.AllowCreateOrder = null;
		else
			ViewBag.AllowCreateOrder = "Allow";
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

			if (response.StatusCode == Domain.Enum.StatusCode.OK) return Json(new {description = response.Description});
			return RedirectToAction("CreateOrder");
		}

		return StatusCode(StatusCodes.Status500InternalServerError);
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IActionResult> DetailOrder(Guid id)
	{
		var response = await _orderService.GetById(id);
		if (response.StatusCode == Domain.Enum.StatusCode.OK) return View(response.Data);
		ModelState.AddModelError("", response.Description);
		return View();
	}

	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> DetailOrder(Order viewModel)
	{
		if (ModelState.IsValid)
		{
			if (viewModel.StatusOrder == StatusOrder.Cancellations &&
			    viewModel.Appointment.DateTimeAppointment > DateTime.Now.AddHours(2))
			{
				var appointmentAsync = await _appointmentService.GetById(viewModel.AppointmentId);
				var appointmentData = appointmentAsync.Data;
				var appointment = new Appointment
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
				var appointment = new Appointment
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

		return RedirectToAction("GetOrders");
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
				var appointment = new Appointment
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

			if (appointmentAsync.StatusAppointment && appointmentAsync.DateTimeAppointment > DateTime.Now.AddHours(2))
			{
				var appointment = new Appointment
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