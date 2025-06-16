using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;
using WebAppProject.Services;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "client")]

    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        // Konstruktor kontrolera OrderController
        public OrderController(ApplicationDbContext context, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Akcja Index zwracająca zawartość koszyka jako zamówienie
        public IActionResult Index()
        {
            // Inicjalizacja modelu widoku OrderVM i pobieranie listy książek w koszyku użytkownika na podstwie ID użytkownika
            var userID = _userManager.GetUserId(User);

            OrderVM orderVM = new OrderVM()
            {
                OrderItems = _context.Carts
                .Include(u => u.book).ThenInclude(b => b.author)
                .Include(u => u.book).ThenInclude(b => b.publisher)
                .Include(u => u.book).ThenInclude(b => b.genre)
                .Include(u => u.book).ThenInclude(b => b.category)
                .Include(u => u.book).ThenInclude(b => b.language)
                .Where(u => u.UserID.Contains(userID)).ToList(),
            };

            // Liczenie ilości elementów w koszyku użytkownika i ustawianie ich w sesji
            var count = _context.Carts.Where(u => u.UserID.Contains(_userManager.GetUserId(User))).ToList().Count;
            HttpContext.Session.SetInt32(CartCount.sessionCount, count);

            return View(orderVM);
        }

        // Akcja SubmitOrder do składania zamówienia
        public IActionResult SubmitOrder()
        {
            // Pobieranie ID zalogowanego użytkownika i książek w koszyku
            var userID = _userManager.GetUserId(User);
            var cartToRemove = _context.Carts.Where(u => u.UserID.Contains(userID)).ToList();

            if (userID != null)
            {
                foreach (var item in cartToRemove)
                {
                    // Tworzenie nowego zamówienia na podstawie książek w koszyku
                    OrderModel newOrder = new OrderModel
                    {
                        BookID = item.BookID,
                        UserID = item.UserID,
                        OrderDate = DateTime.Now,
                    };

                    // Dodawanie nowego zamówienia do bazy danych
                    _context.Orders.AddAsync(newOrder);
                }

                // Usuwanie książek z koszyka po złożeniu zamówienia i zapisanie zmian w bazie danych
                _context.Carts.RemoveRange(cartToRemove);
                _context.SaveChanges();

                // Liczenie ilości elementów w koszyku użytkownika po złożeniu zamówienia i ustawienie ich w sesji
                var count = _context.Carts.Where(u => u.UserID.Contains(userID)).ToList().Count;
                HttpContext.Session.SetInt32(CartCount.sessionCount, count);
            }

            // Ustawianie komunikatu o sukcesie zamówienia
            TempData["SuccessMessage"] = "Your Order has been placed!";

            return RedirectToAction("Index", "Home");
        }
    }
}
