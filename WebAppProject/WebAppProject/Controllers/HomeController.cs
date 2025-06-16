using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAppProject.Data;
using WebAppProject.Models;
using WebAppProject.Services;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;

        // Konstruktor kontrolera HomeController
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // Akcja Index zwracaj�ca stron� g��wn�
        public IActionResult Index(string? searchTitle, int pageNumber = 1, int pageSize = 4)
        {
            // Sprawdzanie, czy u�ytkownik jest zalogowany
            var claim = _signInManager.IsSignedIn(User);

            if (claim)
            {
                // Ustawianie liczby element�w w koszyku u�ytkownika w sesji
                var userID = _userManager.GetUserId(User);
                var count = _context.Carts.Where(u => u.UserID.Contains(userID)).Count();
                HttpContext.Session.SetInt32(CartCount.sessionCount, count);
            }

            //Inicjalizacja modelu widoku
            HomeVM homeVM = new HomeVM();

            //Ustawienie warto�ci searchTitle w ViewData
            ViewData["searchTitle"] = searchTitle;

            //Sprawdzenie, czy podano kryteria wyszukiwania
            if (!string.IsNullOrEmpty(searchTitle))
            {
                // Pobieranie listy ksi��ek z bazy danych
                var booksQuery = _context.Books
                    .Include(u => u.category)
                    .Include(u => u.language)
                    .Include(u => u.author)
                    .Include(u => u.publisher)
                    .Include(u => u.genre)
                    .AsQueryable();

                // Filtrowanie ksi��ek wed�ug tytu�u
                if (!string.IsNullOrEmpty(searchTitle))
                {
                    booksQuery = booksQuery.Where(b => b.Title.Contains(searchTitle));
                }

                // Liczenie ca�kowitej liczby ksi��ek (potrzebne do paginacji)
                var totalBooks = booksQuery.Count();

                // Paginacja
                var books = booksQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                // Ustawienie listy ksi��ek i informacji o paginacji w modelu widoku
                homeVM.BookList = books;
                homeVM.CurrentPage = pageNumber;
                homeVM.TotalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);
            }
            else
            {
                // Je�li nie podano kryteri�w wyszukiwania, ustaw pust� list� ksi��ek
                homeVM.BookList = new List<BookModel>();
            }

            return View(homeVM);

        }


        // GET: Akcja Details wy�wietlaj�ca szczeg�y ksi��ki
        [HttpGet]
        public IActionResult Details(int Id)
        {
            // Pobieranie ksi��ki z bazy danych na podstawie ID i inicjalizacja modelu widoku
            BookVM bookVM = new BookVM()
            {
                Books = _context.Books.FirstOrDefault(p => p.ID == Id),

                CategoriesList = _context.Categories.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.ID.ToString(),
                }),
                LanguagesList = _context.Languages.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.ID.ToString(),
                }),
                AuthorsList = _context.Authors.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.ID.ToString(),
                }),
                PublishersList = _context.Publishers.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.ID.ToString(),
                }),
                GenresList = _context.Genres.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.ID.ToString(),
                }),
            };

            return View(bookVM);
        }

        // Akcja Privacy zwracaj�ca stron� polityki prywatno�ci
        public IActionResult Privacy()
        {
            return View();
        }

        // Akcja Error zwracaj�ca stron� b��du
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
