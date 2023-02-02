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
        public IActionResult CreateTypePet()
        {
            var typePetViewModel = new TypePetViewModel();
            return View(typePetViewModel);
        }

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
        [HttpGet]
        public IActionResult CreateBreedPet()
        {
            var response = _priceCascadingService.GetTypePets();
            var typePets = response.Data;
            ViewBag.TypePet = new SelectList(typePets, "IdTypePet", "typePetName");
            return View();
        }

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
        [HttpGet]
        public IActionResult CreateServiceType()
        {
            var responseTypePet = _priceCascadingService.GetTypePets();
            var typePets = responseTypePet.Data.ToList();
            ViewBag.TypePetBP = new SelectList(typePets, "IdTypePet", "typePetName");
            return View();
        }
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
		public IActionResult GetBreedPets(Guid TypePetId)
        {
	        var response = _priceCascadingService.GetBreedPets();
            var breedPets = response.Data;

            var SubCategory_List = breedPets.Where(s => s.TypePetId == TypePetId).Select(c => new { Id = c.IdBreedPet, Name = c.breedPetName }).ToList();
            //var breedPets = response.Data.Where(x=>x.TypePetId == TypePetId).ToList();
            //ViewBag.BreedPet = new SelectList(breedPets, "IdBreedPet", "breedPetName");
            return Json(SubCategory_List);
		}
	}
}
