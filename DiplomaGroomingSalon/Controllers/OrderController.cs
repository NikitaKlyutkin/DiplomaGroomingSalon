using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Implementations;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		[HttpGet]
		public IActionResult CreateOrder()
		{
			var orderViewModel = new OrderViewModel();
			return View(orderViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder(OrderViewModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _orderService.CreateOrder(model);
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
