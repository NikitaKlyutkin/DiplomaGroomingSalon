using System;
using System.Linq;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaGroomingSalon.Service;

public class ServiceJsController : Controller
{
	private readonly ICRUDDataService<Breed> _breedService;
	private readonly ICRUDDataService<ServiceType> _serviceTypeService;

	public ServiceJsController(ICRUDDataService<Breed> breedService, ICRUDDataService<ServiceType> serviceTypeService)
	{
		_breedService = breedService;
		_serviceTypeService = serviceTypeService;
	}

	public async Task<IActionResult> GetBreedForCascading(Guid petTypeId)
	{
		var response = await _breedService.GetAll();
		var breedPets = response.Data;

		var subCategoryList = breedPets.Where(s => s.PetTypeId == petTypeId)
			.Select(c => new {c.Id, Name = c.BreedName}).ToList();

		return Json(subCategoryList);
	}

	public async Task<IActionResult> GetServiceForCascading(Guid breedId)
	{
		var response = await _serviceTypeService.GetAll();
		var serviceTypes = response.Data;

		var subCategoryList = serviceTypes.Where(s => s.BreedId == breedId)
			.Select(c => new {c.Id, Name = c.ServiceTypeName}).ToList();
		return Json(subCategoryList);
	}

	public async Task<IActionResult> GetPriceForCascading(Guid serviceTypeId)
	{
		var response = await _serviceTypeService.GetAll();
		var serviceTypes = response.Data;

		var subCategoryList = serviceTypes.Where(s => s.Id == serviceTypeId)
			.Select(c => new {c.Id, Name = c.Price}).ToList();
		return Json(subCategoryList);
	}
}