using System;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaGroomingSalon.Controllers;

public class PetTypeController : Controller
{
	private readonly ICRUDDataService<PetType> _petTypeService;

	public PetTypeController(ICRUDDataService<PetType> petTypeService)
	{
		_petTypeService = petTypeService;
	}

	[HttpGet]
	public async Task<IActionResult> GetPetTypes()
	{
		var response = await _petTypeService.GetAll();
		if (response.StatusCode == Domain.Enum.StatusCode.OK) return View(response.Data);
		return View(response.Description);
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	public IActionResult CreatePetType()
	{
		var petTypeViewModel = new PetTypeViewModel();
		return View(petTypeViewModel);
	}

	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> CreatePetType(PetType model)
	{
		if (ModelState.IsValid)
		{
			var response = await _petTypeService.Create(model);
			if (response.StatusCode == Domain.Enum.StatusCode.OK) return Json(new {description = response.Description});
			return RedirectToAction("CreatePetType");
		}

		return StatusCode(StatusCodes.Status500InternalServerError);
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IActionResult> EditPetType(Guid id)
	{
		var response = await _petTypeService.GetById(id);
		if (response.StatusCode == Domain.Enum.StatusCode.OK) return View(response.Data);
		ModelState.AddModelError("", response.Description);
		return View();
	}

	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> EditPetType(PetType viewModel)
	{
		ModelState.Remove("Id");
		ModelState.Remove("PetTypeId");
		if (ModelState.IsValid)
			await _petTypeService.Edit(viewModel.Id, viewModel);
		return RedirectToAction("GetPetTypes");
	}

	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> DeletePetType(Guid id)
	{
		var response = await _petTypeService.Delete(id);
		if (response.StatusCode == Domain.Enum.StatusCode.OK) return RedirectToAction("GetPetTypes");
		return View(response.Description);
	}
}