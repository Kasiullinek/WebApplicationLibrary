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

    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        // Konstruktor kontrolera CartController
        public CartController(ApplicationDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Akcja Index zwracająca zawartość koszyka użytkownika
        public IActionResult Index()
        {
            // Pobieranie książek w koszyku na podstawie ID zalogowanego użytkownika
            var userID = _userManager.GetUserId(User);

            CartVM cartVM = new CartVM()
            {
                CartItems = _context.Carts
                .Include(u => u.book).ThenInclude(b => b.author)
                .Include(u => u.book).ThenInclude(b => b.publisher)
                .Include(u => u.book).ThenInclude(b => b.genre)
                .Include(u => u.book).ThenInclude(b => b.category)
                .Include(u => u.book).ThenInclude(b => b.language)
                .Where(u => u.UserID.Contains(userID)).ToList(),
            };

            // Liczenie ilości elementów w koszyku i ustawianie ich w sesji
            var count = _context.Carts.Where(u => u.UserID.Contains(userID)).Count();
            HttpContext.Session.SetInt32(CartCount.sessionCount, count);

            return View(cartVM);
        }

        //POST: Akcja AddToCart do dodawania książki do koszyka
        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookID)
        {
            // Pobieranie książki na podstawie ID i pobieranie ID zalogowanego użytkownika
            var bookAddToCart = await _context.Books.FirstOrDefaultAsync(u => u.ID == bookID);
            var user = _userManager.GetUserId(User);

            if (user != null)
            {
                // Pobieranie istniejącego koszyka użytkownika
                var getCartWhichExistsForTheUser = await _context.Carts.Where(u => u.UserID.Contains(user)).ToListAsync();

                if (getCartWhichExistsForTheUser.Count() > 0)
                {
                    // Sprawdzanie, czy książka już jest w koszyku
                    var getTheQuantity = getCartWhichExistsForTheUser.FirstOrDefault(p => p.BookID == bookID);

                    if (getTheQuantity != null)
                    {
                        // Ustawianie komunikatu o błędzie
                        TempData["AlertMessage"] = "Book is already in the cart!";
                    }
                    else
                    {
                        // Ustawianie komunikatu o sukcesie
                        TempData["AlertMessage"] = "New book has been added to the cart!";
                        CartModel newBookToCart = new CartModel
                        {
                            BookID = bookID,
                            UserID = user,
                        };

                        // Dodawanie nowej książki do koszyka
                        await _context.Carts.AddAsync(newBookToCart);
                    }
                }
                else
                {
                    // Ustawianie komunikatu o sukcesie
                    TempData["AlertMessage"] = "New book has been added to the cart!";
                    CartModel newBookToCart = new CartModel
                    {
                        BookID = bookID,
                        UserID = user,
                    };

                    // Dodawanie nowej książki do koszyka
                    await _context.Carts.AddAsync(newBookToCart);
                }

                // Zapisanie zmian w bazie danych
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        // Akcja RemoveFromCart do usuwania książki z koszyka
        public IActionResult RemoveFromCart(int bookID)
        {
            // Pobieranie książki z koszyka na podstawie ID książki
            var bookToRemove = _context.Carts.FirstOrDefault(u => u.BookID == bookID);

            if (bookToRemove != null)
            {
                // Usuwanie książki z koszyka i zapisanie zmian
                _context.Carts.Remove(bookToRemove);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
