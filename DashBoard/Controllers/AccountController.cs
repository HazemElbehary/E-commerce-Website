using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.Controllers
{
	public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
	{
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
		{

			var user = await userManager.FindByEmailAsync(dto.Email);

			if (user is null)
			{
				ModelState.AddModelError("Email", "Invalid Login");
				return View(dto);
			}

			var checkPassword = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

			if (checkPassword.IsNotAllowed)
            {
                ModelState.AddModelError("Email", "Please Confirm You Email");
                return View(dto);
            }

			if (checkPassword.IsLockedOut)
            {
                ModelState.AddModelError("Email", "Your Account Is LockOut");
                return View(dto);
            }

			if (!checkPassword.Succeeded)
            {
                ModelState.AddModelError("Email", "Invalid Login");
                return View(dto);
            }

			await signInManager.SignInAsync(user, true);

			return RedirectToAction("Index", "Home");

		}

		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
