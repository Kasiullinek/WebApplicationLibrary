using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;
using WebAppProject.Models;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;

        // Konstruktor kontrolera AccountController
        public AccountController(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // Akcja Index wyświetlająca widok domyślny
        public IActionResult Index()
        {
            return View();
        }

        // GET: Akcja Login wyświetlająca formularz logowania
        [HttpGet]
        public IActionResult Login()
        {
            LoginVM vm = new LoginVM();

            return View(vm);
        }

        // POST: Akcja Login przetwarzająca dane logowania
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                // Sprawdzenie poprawności danych logowania użytkownika
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Przekierowanie do strony głównej w przypadku udanego logowania
                    TempData["SuccessMessage"] = "Login Process is Succesfull!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Ustawienie komunikatu o niepowodzeniu logowania
                    TempData["ErrorMessage"] = "Login Process Failed!";
                }
            }
            else
            {
                // Ustawienie komunikatu o niepowodzeniu logowania
                TempData["ErrorMessage"] = "Login Process Failed!";
            }

            return View(login);
        }

        // GET: Akcja Register wyświetlająca formularz rejestracji
        [HttpGet]
        public IActionResult Register()
        {
            RegisterVM vm = new RegisterVM();
            return View(vm);
        }

        // POST: Akcja Register przetwarzająca dane rejestracyjne
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                // Tworzenie nowego użytkownika na podstawie danych rejestracyjnych
                var user = new UserModel
                {
                    FirstName = register.userModel.FirstName,
                    LastName = register.userModel.LastName,
                    DateOfBirth = register.userModel.DateOfBirth,
                    PESEL = register.userModel.PESEL,
                    Address = register.userModel.Address,
                    PhoneNumber = register.PhoneNumber,
                    Email = register.Email,
                    UserName = register.Email,

                };

                // Rejestracja nowego użytkownika
                var Registration = await _userManager.CreateAsync(user, register.Password);

                if (Registration.Succeeded)
                {
                    // Dodanie użytkownika do roli "client", jego zalogowanie i wyświetlenie komunikatu
                    await _userManager.AddToRoleAsync(user, "client");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["SuccessMessage"] = "Registration is Succesfull!";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Ustawienie komunikatu o niepowodzeniu rejestracji
                    TempData["ErrorMessage"] = "Registration Failed!";
                }
            }
            else
            {
                // Ustawienie komunikatu o niepowodzeniu rejestracji
                TempData["ErrorMessage"] = "Registration Failed!";
            }

            return View(register);  
        }

        // POST: Akcja Logout wylogowująca użytkownika
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Wyczyszczenie sesji i wylogowanie użytkownika
            HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
