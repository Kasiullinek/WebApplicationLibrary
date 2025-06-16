using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        // Konstruktor kontrolera ReportController
        public ReportController(ApplicationDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Akcja Index wyświetlająca raporty z filtrowaniem
        public IActionResult Index(ReportVM? filters, int pageNumber = 1, int pageSize = 10)
        {
            // Pobranie list filtrów z bazy danych
            var reportVM = new ReportVM
            {
                CategoriesList = _context.Categories.ToList(),
                GenresList = _context.Genres.ToList(),
                LanguagesList = _context.Languages.ToList(),
                AuthorsList = _context.Authors.ToList(),
                PublishersList = _context.Publishers.ToList(),

                SelectedCategories = filters?.SelectedCategories,
                SelectedGenres = filters?.SelectedGenres,
                SelectedLanguages = filters?.SelectedLanguages,
                SelectedAuthors = filters?.SelectedAuthors,
                SelectedPublishers = filters?.SelectedPublishers,
                StartDate = filters?.StartDate,
                EndDate = filters?.EndDate,
                SearchUserName = filters?.SearchUserName
            };

            // Inicjalizacja zapytania o zamówienia
            var query = _context.Orders
                .Include(o => o.book).ThenInclude(b => b.author)
                .Include(o => o.book).ThenInclude(b => b.publisher)
                .Include(o => o.book).ThenInclude(b => b.genre)
                .Include(o => o.book).ThenInclude(b => b.category)
                .Include(o => o.book).ThenInclude(b => b.language)
                .Include(o => o.user)
                .AsQueryable();

            // Filtrowanie według wybranych filtrów
            if (filters?.SelectedCategories != null && filters.SelectedCategories.Any())
            {
                query = query.Where(o => filters.SelectedCategories.Contains(o.book.CategoryID));
            }

            if (filters?.SelectedGenres != null && filters.SelectedGenres.Any())
            {
                query = query.Where(o => filters.SelectedGenres.Contains(o.book.GenreID));
            }

            if (filters?.SelectedLanguages != null && filters.SelectedLanguages.Any())
            {
                query = query.Where(o => filters.SelectedLanguages.Contains(o.book.LanguageID));
            }

            if (filters?.SelectedAuthors != null && filters.SelectedAuthors.Any())
            {
                query = query.Where(o => filters.SelectedAuthors.Contains(o.book.AuthorID));
            }

            if (filters?.SelectedPublishers != null && filters.SelectedPublishers.Any())
            {
                query = query.Where(o => filters.SelectedPublishers.Contains(o.book.PublisherID));
            }

            // Filtrowanie według zakresu dat
            if (filters?.StartDate.HasValue == true && filters.EndDate.HasValue == true) 
            { 
                query = query.Where(o => o.OrderDate >= filters.StartDate.Value && o.OrderDate <= filters.EndDate.Value); 
            }

            // Filtrowanie według nazwy użytkownika
            if (!string.IsNullOrEmpty(filters?.SearchUserName)) 
            { 
                query = query.Where(o => o.user.UserName.Contains(filters.SearchUserName)); 
            }

            // Liczenie całkowitej liczby zamówień
            var totalOrders = query.Count();

            // Paginacja
            var orders = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Dodanie przefiltrowanej i stronicowanej listy zamówień do widoku
            reportVM.OrderList = orders;
            reportVM.CurrentPage = pageNumber;
            reportVM.TotalPages = (int)Math.Ceiling(totalOrders / (double)pageSize);

            return View(reportVM);
        }

    }
}
