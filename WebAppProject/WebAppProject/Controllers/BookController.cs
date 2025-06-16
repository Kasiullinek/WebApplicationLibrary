using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Konstruktor kontrolera BookController
        public BookController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // Akcja Index zwracająca listę książek
        public IActionResult Index(string searchTitle, string searchISBN, int pageNumber = 1, int pageSize = 5)
        {
            var booksQuery = _context.Books
                .Include(u => u.category)
                .Include(u => u.language)
                .Include(u => u.author)
                .Include(u => u.publisher)
                .Include(u => u.genre)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTitle))
            {
                booksQuery = booksQuery.Where(b => b.Title.Contains(searchTitle)); 
            }

            if (!string.IsNullOrEmpty(searchISBN)) 
            {
                booksQuery = booksQuery.Where(b => b.ISBN.Contains(searchISBN)); 
            }

            // Liczenie całkowitej liczby książek (potrzebne do paginacji)
            var totalBooks = booksQuery.Count();

            // Paginacja
            var books = booksQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Przekazanie danych do ViewBag
            ViewBag.CurrentPage = pageNumber; 
            ViewBag.TotalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);
            ViewBag.SearchTitle = searchTitle; 
            ViewBag.SearchISBN = searchISBN; 
            
            return View(books);

        }

        // GET: Akcja Add wyświetlająca formularz dodawania książki
        [HttpGet]
        public IActionResult Add()
        {
            // Inicjalizacja modelu widoku z listami kategorii wiekowej, języków, autorów, wydawców i gatunków
            BookVM bookVM = new BookVM()
            {
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

        // POST: Akcja Add przetwarzająca dane dodawania książki
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BookVM bookVM)
        {
            if (ModelState.IsValid)
            {
                // Sprawdzanie, czy książka o podanym ISBB już istnieje
                var foundItem = await _context.Books.FirstOrDefaultAsync(u => u.ISBN == bookVM.Books.ISBN);

                if (foundItem != null)
                {
                    TempData["AlertMessage"] = "Book with given ISBN already exists! Book has not been added to the datebase.";
                    return RedirectToAction("Index", "Book");
                }

                // Przesyłanie pliku obrazu, jeśli jest dostępny
                string imageUrl = "";

                if (bookVM.ImageFile != null)
                {
                    imageUrl = UploadFile(bookVM.ImageFile);
                }

                bookVM.Books.ImgUrl = imageUrl;

                // Dodawanie nowej książki do bazy danych i zapisanie zmian
                await _context.AddAsync(bookVM.Books);
                await _context.SaveChangesAsync();

                // Ustawienie komunikatu o powodzeniu
                TempData["AlertMessage"] = "Proccess is Successful!";
            }
            else
            {
                // Ustawienie komunikatu o niepowodzeniu
                TempData["AlertMessage"] = "Model State Validation Failed! Entered data was not consistent with the book model. ";
            }

            return RedirectToAction("Index", "Book");
        }

        // GET: Akcja Edit wyświetlająca formularz edycji książki
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            // Pobieranie książki z bazy danych na podstawie ID i inicjalizacja modelu widoku
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

        // POST: Akcja Edit przetwarzająca dane edytowanej książki
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(BookVM bookVM)
        {
            if (ModelState.IsValid)
            {
                var BookToEdit = _context.Books.FirstOrDefault(u => u.ID == bookVM.Books.ID);

                if (BookToEdit != null)
                {
                    // Sprawdzanie, czy książka o podanym ISBN już istnieje, z wykluczeniem aktualnie edytowanej książki
                    var foundItem = _context.Books.FirstOrDefault(u => u.ISBN == bookVM.Books.ISBN && u.ID != bookVM.Books.ID);

                    if (foundItem != null)
                    {
                        TempData["AlertMessage"] = "Book with given ISBN already exists!";
                        return RedirectToAction("Index", "Book");
                    }

                    // Aktualizacja danych książki
                    BookToEdit.Title = bookVM.Books.Title;
                    BookToEdit.AuthorID = bookVM.Books.AuthorID;
                    BookToEdit.Description = bookVM.Books.Description;
                    BookToEdit.PublishDate = bookVM.Books.PublishDate;
                    BookToEdit.PublisherID = bookVM.Books.PublisherID;
                    BookToEdit.GenreID = bookVM.Books.GenreID;
                    BookToEdit.LanguageID = bookVM.Books.LanguageID;
                    BookToEdit.CategoryID = bookVM.Books.CategoryID;
                    BookToEdit.ISBN = bookVM.Books.ISBN;

                    // Przesyłanie nowego pliku obrazu, jeśli jest dostępny
                    if (bookVM.ImageFile != null)
                    {
                        if (BookToEdit.ImgUrl != "")
                        {
                            var oldImageUrl = "img\\" + BookToEdit.ImgUrl;
                            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, oldImageUrl.TrimStart('\\'));

                            // Usuwanie starego pliku obrazu
                            DeleteImage(oldImagePath);
                        }
                        string newImageUrl = UploadFile(bookVM.ImageFile);
                        BookToEdit.ImgUrl = newImageUrl;
                    }

                    // Aktualizacja i zapisanie książki w bazie danych
                    _context.Books.Update(BookToEdit);
                    _context.SaveChanges();

                    // Ustawienie komunikatu o powodzeniu
                    TempData["AlertMessage"] = "Proccess is Successful!";
                }

            }
            else
            {
                // Ustawienie komunikatu o niepowodzeniu
                TempData["AlertMessage"] = "Model State Validation Failed! New entered data was not consistent with the book model. ";
            }

            return RedirectToAction("Index", "Book");
        }

        // GET: Akcja Delete wyświetlająca formularz usuwania książki
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var BookToDelete = _context.Books.FirstOrDefault(u => u.ID == Id);

            if (BookToDelete.ImgUrl != "")
            {
                var imageUrl = "img\\" + BookToDelete.ImgUrl;
                var toDeleteImageFromFolder = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('\\'));

                // Usuwanie pliku obrazu
                DeleteImage(toDeleteImageFromFolder);
            }

            // Usuwanie książki z bazy danych i zampisanie zmian
            _context.Books.Remove(BookToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index", "Book");
        }

        // Metoda do usuwania pliku obrazu z folderu
        private void DeleteImage(string toDeleteImageFromFolder)
        {
            if (System.IO.File.Exists(toDeleteImageFromFolder))
            {
                // Sprawdzanie i usuwanie pliku obrazu
                System.IO.File.Exists(toDeleteImageFromFolder);
            }
        }

        // Metoda do przesyłania pliku obrazu
        private string UploadFile(IFormFile image)
        {
            string fileName = null;

            if (image != null)
            {
                string uploadDirLocation = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadDirLocation, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // Kopiowanie pliku obrazu do wyznaczonej lokalizacji
                    image.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}
