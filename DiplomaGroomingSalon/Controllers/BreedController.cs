using System;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplomaGroomingSalon.Controllers;

public class BreedController : Controller
{
	private readonly ICRUDDataService<Breed> _breedService;
	private readonly ICRUDDataService<PetType> _petTypeService;

	public BreedController(ICRUDDataService<Breed> breedService, ICRUDDataService<PetType> petTypeService)
	{
		_breedService = breedService;
		_petTypeService = petTypeService;
	}

	[HttpGet]
	public async Task<IActionResult> GetBreeds()
	{
		var response = await _breedService.GetAll();

		if (response.StatusCode == Domain.Enum.StatusCode.OK) return View(response.Data);
		return View(response.Description);
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IActionResult> CreateBreed()
	{
		var petType = await _petTypeService.GetAll();
		var dataPetType = petType.Data;

		if (dataPetType != null)
		{
			var response = await _petTypeService.GetAll();
			var typePets = response.Data;
			ViewBag.TypePet = new SelectList(typePets, "Id", "PetTypeName");
			return View();
		}

		ViewBag.TypePet = null;

		return View();
	}

	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> CreateBreed(Breed model)
	{
		if (ModelState.IsValid)
		{
			var response = await _breedService.Create(model);

			if (response.StatusCode == Domain.Enum.StatusCode.OK) return Json(new {description = response.Description});
			return RedirectToAction("CreateBreed");
		}

		return StatusCode(StatusCodes.Status500InternalServerError);
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IActionResult> EditBreed(Guid id)
	{
		var response = await _breedService.GetById(id);
		if (response.StatusCode == Domain.Enum.StatusCode.OK) return View(response.Data);
		ModelState.AddModelError("", response.Description);
		return View();
	}

	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> EditBreed(Breed viewModel)
	{
		ModelState.Remove("Id");
		ModelState.Remove("PetTypeId");
		if (ModelState.IsValid)
			await _breedService.Edit(viewModel.Id, viewModel);
		return RedirectToAction("GetBreeds");
	}

	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> DeleteBreed(Guid id)
	{
		var response = await _breedService.Delete(id);
		if (response.StatusCode == Domain.Enum.StatusCode.OK) return RedirectToAction("GetBreeds");
		return View(response.Description);
	}
}