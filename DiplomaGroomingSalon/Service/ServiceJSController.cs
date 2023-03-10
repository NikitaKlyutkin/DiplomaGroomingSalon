using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace DiplomaGroomingSalon.Service
{
	public class ServiceJSController : Controller
	{
		private readonly ICRUDDataService<Breed> _breedService;
		private readonly ICRUDDataService<ServiceType> _serviceTypeService;
		private readonly IAppointmentService _appointmentService;
		public ServiceJSController(ICRUDDataService<Breed> breedService, ICRUDDataService<ServiceType> serviceTypeService, IAppointmentService appointmentService)
		{
			_breedService = breedService;
			_serviceTypeService = serviceTypeService;
			_appointmentService = appointmentService;
		}
		public async Task<IActionResult> GetBreedForCascading(Guid PetTypeId)
		{
			var response = await _breedService.GetAll();
			var breedPets = response.Data;

			var SubCategory_List = breedPets.Where(s => s.PetTypeId == PetTypeId)
				.Select(c => new { Id = c.Id, Name = c.BreedName }).ToList();

			return Json(SubCategory_List);
		}

		public async Task<IActionResult> GetServiceForCascading(Guid BreedId)
		{
			var response = await _serviceTypeService.GetAll();
			var serviceTypes = response.Data;

			var SubCategory_List = serviceTypes.Where(s => s.BreedId == BreedId)
				.Select(c => new { Id = c.Id, Name = c.ServiceTypeName }).ToList();
			return Json(SubCategory_List);
		}

		public async Task<IActionResult> GetPriceForCascading(Guid ServiceTypeId)
		{
			var response = await _serviceTypeService.GetAll();
			var serviceTypes = response.Data;

			var SubCategory_List = serviceTypes.Where(s => s.Id == ServiceTypeId)
				.Select(c => new { Id = c.Id, Name = c.Price }).ToList();
			return Json(SubCategory_List);
		}
	}
}
