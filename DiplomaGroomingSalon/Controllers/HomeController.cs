using DiplomaGroomingSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL;

namespace DiplomaGroomingSalon.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly DBContext _context;
		public HomeController(ILogger<HomeController> logger, DBContext context)
		{
			_logger = logger;
			_context = context;

		}

		public IActionResult Index()
		{
			//var createDB = _context.Users.ToList();
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
