using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaGroomingSalon.Controllers;

public class AccountController : Controller
{
	private readonly IAccountService _accountService;

	public AccountController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	[HttpGet]
	public IActionResult Register()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Register(RegisterViewModel model)
	{
		if (ModelState.IsValid)
		{
			var response = await _accountService.Register(model);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(response.Data));

				return RedirectToAction("Detail", "Profile");
			}

			ModelState.AddModelError("", response.Description);
		}

		return View(model);
	}

	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		if (ModelState.IsValid)
		{
			var response = await _accountService.Login(model);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(response.Data));

				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", response.Description);
		}

		return View(model);
	}

	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return RedirectToAction("Index", "Home");
	}

	[HttpPost]
	public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
	{
		if (ModelState.IsValid)
		{
			var response = await _accountService.ChangePassword(model);
			if (response.StatusCode == Domain.Enum.StatusCode.OK) return Json(new {description = response.Description});
		}

		var modelError = ModelState.Values.SelectMany(v => v.Errors);

		return StatusCode(StatusCodes.Status500InternalServerError, new {modelError.FirstOrDefault().ErrorMessage});
	}
}