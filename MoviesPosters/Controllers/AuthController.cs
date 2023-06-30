using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesPosters.Models;
using MoviesPosters.ViewModels;

namespace MoviesPosters.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotyfService _notyfService;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, INotyfService notyfService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notyfService = notyfService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Image");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Image");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if( await CheckDuplicateEmail(vm.Email!))
            {
                _notyfService.Error($"Email: {vm.Email} already exists! Use a different email");
                return View(vm);
            }
            if(await CheckDuplicateUsername(vm.UserName!))
            {
                _notyfService.Error($"UserName: {vm.UserName} already exists! Use a different email");
                return View(vm);
            }
            var user = new ApplicationUser
            {
                UserName = vm.UserName,
                Email = vm.Email,
                FullName = vm.FullName,
            };
            await _userManager.CreateAsync(user, vm.Password!);
            _notyfService.Success("Successful Registration");
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var userByUserName = await _userManager.FindByNameAsync(vm.UserName!);
            if (userByUserName == null)
            {
                _notyfService.Error("Invalid Username.");
                return View(vm);
            }
            var verifyPassword = await _userManager.CheckPasswordAsync(userByUserName, vm.Password!);
            if(!verifyPassword) 
            {
                _notyfService.Error("Wrong Password");
                return View(vm);
            }
            await _signInManager.SignInAsync(userByUserName, vm.RememberMe);
            return RedirectToAction(nameof(Index), "Image");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _notyfService.Success("Logout Successfully");
            return RedirectToAction(nameof(Index), "Home");
        }

        private async Task<bool> CheckDuplicateEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> CheckDuplicateUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user != null)
            {
                return true;
            }
            return false;
        }
    }
}
