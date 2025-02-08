using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                // Если пользователь найден, проверяем пароль
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                if (passwordCheck)
                {
                    // Если пароль верный, вход в систему
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                // Если пароль неверный
                TempData["Error"] = "Неверные данные. Пожалуйста, попробуйте снова";
                return View(loginViewModel);
            }

            // Пользователь не найден
            TempData["Error"] = "Неверные данные. Пожалуйста, попробуйте снова";
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            // Проверяем есть ли пользователь с таким email.
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "Эта электронная почта уже используется";
                return View(registerViewModel);
            }
            else
            { 
                var newUser = new AppUser()
                {
                    Email = registerViewModel.EmailAddress,
                    UserName = registerViewModel.EmailAddress 
                };

                var resultAppUser = await _userManager.CreateAsync(newUser, registerViewModel.Password);
                if (resultAppUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
                else
                {
                    // Для отладки: вывод ошибок в консоль или лог
                    foreach (var error in resultAppUser.Errors)
                    {
                        Console.WriteLine($"Ошибка: {error.Code} - {error.Description}");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
