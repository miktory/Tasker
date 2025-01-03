using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tasker.Identity.Models;

namespace Tasker.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, 
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded) 
            {
                return Redirect(model.ReturnUrl);
            }
			ModelState.AddModelError(string.Empty, "Login error");
			return View(model);
        }

		[HttpGet]
		public IActionResult Register(string returnUrl)
		{
			var viewModel = new RegisterViewModel
			{
				ReturnUrl = returnUrl,
			};

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

            var user = new AppUser
            {
                UserName = model.Username,
                FirstName = " ",
                LastName = " "
            };
			var result = await _userManager.CreateAsync(user, model.Password);

		

			if (result.Succeeded)
			{
				var roles = new List<string>();
				roles.Add("User");
				if (model.IsAdmin)
					roles.Add("Admin");
				await _userManager.AddToRolesAsync(user, roles.ToArray());
				_signInManager.SignInAsync(user, false);
				return Redirect(model.ReturnUrl);
			}
			ModelState.AddModelError(string.Empty, "Registration error.");
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Logout(string logoutId)
		{
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
		}
	}
}
