using AgencyBackend.Entities;
using AgencyBackend.ViewModel.AuthViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBackend.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) return View(register);
            ApplicationUser newUser = new ApplicationUser
            {
                Fullname = register.Fullname,
                Email = register.Email,
                UserName = register.Username
            };

            IdentityResult identityResult = await _userManager.CreateAsync(newUser, register.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(string? returnUrl, LoginViewModel login)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (!ModelState.IsValid) return View(login);
            ApplicationUser user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "Email or Password is Wrong");
                return View(login);
            }

            if (!user.IsActivated)
            {
                ModelState.AddModelError(String.Empty, "Please Activate Your Email(Check Your Email)");
                return View(login);
            }

            var signinresult = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

            if (signinresult.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty, "Please wait(You banned)");
                return View(login);
            }

            if (!signinresult.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Email or Password is Wrong");
                return View(login);
            }
            return Redirect(returnUrl);

        }


    }
}
