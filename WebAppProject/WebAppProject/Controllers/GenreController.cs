using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor kontrolera GenreController
        public GenreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Akcja Index zwracająca listę gatunków
        public IActionResult Index()
        {
            var items = _context.Genres.OrderBy(g => g.Name).ToList();

            return View(items);
        }

        // GET: Akcja Upsert do dodawania lub edytowania gatunku
        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                // Tworzenie nowego obiektu GenreModel
                GenreModel genre = new GenreModel();
                return View(genre);
            }
            else
            {
                // Pobieranie gatunku z bazy danych na podstawie ID
                var items = _context.Genres.FirstOrDefault(u => u.ID == id);
                return View(items);
            }
        }

        // POST: Akcja Upsert do dodawania lub edytowania gatunku
        [HttpPost]
        public async Task<IActionResult> Upsert(int? id, GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    // Sprawdzanie, czy gatunek o podanej nazwie już istnieje
                    var foundItem = await _context.Genres.FirstOrDefaultAsync(u => u.Name == genre.Name);

                    if (foundItem != null)
                    {
                        TempData["AlertMessage"] = "'" + genre.Name + "' exists in the list! It hasn't been added to the list";
                        return RedirectToAction("Index");
                    }

                    // Dodawanie nowego gatunku do bazy danych
                    await _context.Genres.AddAsync(genre);
                    TempData["AlertMessage"] = "'" + genre.Name + "' has been added to the list successfully";
                }
                else
                {
                    // Sprawdzanie, czy nowa nazwa gatunku już istnieje, ale wykluczając bieżący gatunek
                    var foundItem = await _context.Genres.FirstOrDefaultAsync(u => u.Name == genre.Name && u.ID != id);
                    if (foundItem != null)
                    {
                        TempData["AlertMessage"] = "'" + genre.Name + "' exists in the list! It hasn't been changed";
                        return RedirectToAction("Index");
                    }

                    // Aktualizacja nazwy gatunku
                    var items = await _context.Genres.FirstOrDefaultAsync(u => u.ID == id);
                    items.Name = genre.Name;
                    TempData["AlertMessage"] = "'" + genre.Name + "' has been edited";
                }

                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }

        // GET: Akcja Delete do usuwania gatunku
        public IActionResult Delete(int id)
        {
            var item = _context.Genres.FirstOrDefault(u => u.ID == id);
            return View(item);
        }

        // POST: Akcja Delete do usuwania gatunku
        [HttpPost]
        public async Task<IActionResult> Delete(GenreModel genre)
        {
            var item = _context.Genres.FirstOrDefault(u => u.ID == genre.ID);

            TempData["AlertMessage"] = "'" + item.Name + "' has been deleted successfully";
            _context.Genres.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}

