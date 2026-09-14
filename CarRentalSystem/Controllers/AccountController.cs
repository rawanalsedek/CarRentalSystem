using BLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Filters;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVm());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (ModelState.IsValid)
            {
                var user = new User() { Email = vm.Email, UserName = vm.UserName, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, vm.Password ?? string.Empty);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError(identityError.Code, identityError.Description);
                }
            }

            return View(vm);
        }

        [AllowAnonymousOnly]
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginVm());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await FindUserAsync(vm.UserName);

                if (user != null && !string.IsNullOrEmpty(vm.Password))
                {
                    var result = await _signInManager.PasswordSignInAsync(
                        user,
                        vm.Password,
                        vm.RememberMe,
                        false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        if (await _userManager.IsInRoleAsync(user, "Super"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }

                        return RedirectToAction("MyReservations", "Reservation");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }

            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task<User?> FindUserAsync(string? userNameOrEmail)
        {
            if (string.IsNullOrWhiteSpace(userNameOrEmail))
            {
                return null;
            }

            return await _userManager.FindByNameAsync(userNameOrEmail)
                ?? await _userManager.Users.FirstOrDefaultAsync(user => user.Email == userNameOrEmail);
        }
    }
}
