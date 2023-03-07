using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Controllers
{
	public class ServiceTypeController : Controller
	{
		private readonly ICRUDDataService<PetType> _petTypeService;
		private readonly ICRUDDataService<ServiceType> _serviceTypeService;

		public ServiceTypeController(ICRUDDataService<PetType> petTypeService, ICRUDDataService<ServiceType> serviceTypeService)
		{
			_petTypeService = petTypeService;
			_serviceTypeService = serviceTypeService;
		}
		[HttpGet]
		public async Task<IActionResult> GetServiceTypes()
		{
			var response = await _serviceTypeService.GetAll();
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return View(response.Description);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> CreateServiceType()
		{
			var petType = await _petTypeService.GetAll();
			var dataPetType = petType.Data;

			var breed = await _petTypeService.GetAll();
			var dataBreed = breed.Data;

			if (dataPetType != null && dataBreed != null)
			{
				ViewBag.BreedPetBP = "true";
				ViewBag.TypePetBP = new SelectList(dataPetType, "Id", "PetTypeName");
				return View();
			}
			else if (dataBreed == null)
			{
				ViewBag.TypePetBP = "true";
				ViewBag.BreedPetBP = null;
				return View();
			}
			else
			{
				ViewBag.TypePetBP = null;
				return View();
			}

		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> CreateServiceType(ServiceType model)
		{
			if (ModelState.IsValid)
			{
				var response = await _serviceTypeService.Create(model);

				if (response.StatusCode == Domain.Enum.StatusCode.OK)
				{
					return Json(new { description = response.Description });
				}
				return RedirectToAction("CreateServiceType");

			}
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> EditServiceType(Guid id)
		{
			var response = await _serviceTypeService.GetById(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			ModelState.AddModelError("", response.Description);
			return View();
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> EditServiceType(ServiceType viewModel)
		{
			ModelState.Remove("Id");
			ModelState.Remove("PetTypeId");
			ModelState.Remove("BreedId");
			if (ModelState.IsValid)
				await _serviceTypeService.Edit(viewModel.Id, viewModel);
			return RedirectToAction("GetServiceTypes");
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteServiceTypes(Guid id)
		{
			var response = await _serviceTypeService.Delete(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return RedirectToAction("GetServiceTypes");
			}
			return View(response.Description);
		}
	}
}
