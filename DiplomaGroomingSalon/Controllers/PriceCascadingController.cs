using System;
using System.Collections.Generic;
using System.Linq;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Implementations;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Entities;
using static MessagePack.MessagePackSerializer;
using NuGet.Protocol.Core.Types;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace DiplomaGroomingSalon.Controllers
{
    public class PriceCascadingController : Controller
	{
		private readonly IPriceCascadingService _priceCascadingService;

		public PriceCascadingController(IPriceCascadingService priceCascadingService)
		{
			_priceCascadingService = priceCascadingService;
		}
		[HttpGet]
		public IActionResult GetTypePets()
		{
			var response = _priceCascadingService.GetTypePets();
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return View(response.Description);
		}
		[HttpGet]
		public IActionResult GetBreedPets()
        {
            var response = _priceCascadingService.GetBreedPets();

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
			return View(response.Description);
		}
		[HttpGet]
		public IActionResult GetServiceTypes()
		{
			var response = _priceCascadingService.GetServiceTypes();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response.Data);
			}
			return View(response.Description);
		}
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateTypePet()
        {
            var typePetViewModel = new TypePetViewModel();
            return View(typePetViewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTypePet(TypePetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _priceCascadingService.CreateTypePet(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
                return RedirectToAction("CreateTypePet");

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateBreedPet()
        {
	        if (_priceCascadingService.GetTypePets().Data != null)
	        {
				var response = _priceCascadingService.GetTypePets();
				var typePets = response.Data;
				ViewBag.TypePet = new SelectList(typePets, "IdTypePet", "typePetName");
				return View();
			}

	        ViewBag.TypePet = null;

			return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBreedPet(BreedPetViewModel model)
        {
            if (ModelState.IsValid)
            {

                var response = await _priceCascadingService.CreateBreedPet(model);

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
                return RedirectToAction("CreateBreedPet");

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateServiceType()
        {
	        if (_priceCascadingService.GetTypePets().Data != null  && _priceCascadingService.GetBreedPets().Data != null)
	        {
				var responseTypePet = _priceCascadingService.GetTypePets();
				var typePets = responseTypePet.Data.ToList();
				ViewBag.BreedPetBP = "true";
				ViewBag.TypePetBP = new SelectList(typePets, "IdTypePet", "typePetName");
				return View();
			}
	        else if(_priceCascadingService.GetBreedPets().Data == null)
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
		public async Task<IActionResult> CreateServiceType(ServiceTypeViewModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _priceCascadingService.CreateServiceType(model);

				if (response.StatusCode == Domain.Enum.StatusCode.OK)
				{
					return Json(new { description = response.Description });
				}
				return RedirectToAction("CreateServiceType");

			}
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		public IActionResult GetBreedForCascading(Guid TypePetId)
        {
	        var response = _priceCascadingService.GetBreedPets();
            var breedPets = response.Data;

            var SubCategory_List = breedPets.Where(s => s.TypePetId == TypePetId)
	            .Select(c => new { Id = c.IdBreedPet, Name = c.breedPetName }).ToList();

            return Json(SubCategory_List);
		}
		public IActionResult GetServiceForCascading(Guid BreedPetId)
		{
			var response = _priceCascadingService.GetServiceTypes();
			var serviceTypes = response.Data;

			var SubCategory_List = serviceTypes.Where(s => s.BreedPetId == BreedPetId)
				.Select(c => new { Id = c.IdServiceType, Name = c.serviceTypeName }).ToList();
			return Json(SubCategory_List);
		}
		public IActionResult GetPriceForCascading(Guid ServiceTypeId)
		{
			var response = _priceCascadingService.GetServiceTypes();
			var serviceTypes = response.Data;

			var SubCategory_List = serviceTypes.Where(s => s.IdServiceType == ServiceTypeId)
				.Select(c => new { Id = c.IdServiceType, Name = c.Price }).ToList();
			return Json(SubCategory_List);
		}
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditTypePet(Guid id)
        {
            var response = await _priceCascadingService.GetTypePet(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditTypePet(TypePetViewModel viewModel)
        {
            ModelState.Remove("IdTypePet");
            ModelState.Remove("TypePetId");
            if (ModelState.IsValid)
                await _priceCascadingService.EditTypePet(viewModel.IdTypePet, viewModel);
            return RedirectToAction("GetTypePets");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditBreedPet(Guid id)
        {
            var response = await _priceCascadingService.GetBreedPet(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditBreedPet(BreedPetViewModel viewModel)
        {
            ModelState.Remove("IdBreedPet");
            ModelState.Remove("TypePetId");
            if (ModelState.IsValid)
                await _priceCascadingService.EditBreedPet(viewModel.IdBreedPet, viewModel);
            return RedirectToAction("GetBreedPets");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditServiceType(Guid id)
        {
            var response = await _priceCascadingService.GetServiceType(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditServiceType(ServiceTypeViewModel viewModel)
        {
            ModelState.Remove("IdServiceType");
            ModelState.Remove("TypePetId");
            ModelState.Remove("BreedPetId");
            if (ModelState.IsValid)
                await _priceCascadingService.EditServiceType(viewModel.IdServiceType, viewModel);
            return RedirectToAction("GetServiceTypes");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTypePets(Guid id)
        {
            var response = await _priceCascadingService.DeleteTypePet(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetTypePets");
            }
            return View(response.Description);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBreedPets(Guid id)
        {
            var response = await _priceCascadingService.DeleteBreedPet(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetBreedPets");
            }
            return View(response.Description);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteServiceTypes(Guid id)
        {
	        var response = await _priceCascadingService.DeleteServiceType(id);
	        if (response.StatusCode == Domain.Enum.StatusCode.OK)
	        {
		        return RedirectToAction("GetServiceTypes");
	        }
	        return View(response.Description);
        }

	}
}
