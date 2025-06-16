using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor kontrolera AuthorController
        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Akcja Index zwracająca listę autorów
        public IActionResult Index()
        {
            var items = _context.Authors.OrderBy(a => a.Name).ToList();

            return View(items);
        }

        // GET: Akcja Upsert do dodawania lub edytowania autora
        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                // Tworzenie nowego obiektu AuthorModel
                AuthorModel author = new AuthorModel();
                return View(author);
            }
            else
            {
                // Pobieranie autora z bazy danych na podstawie ID
                var items = _context.Authors.FirstOrDefault(u => u.ID == id);
                return View(items);
            }
        }

        // POST: Akcja Upsert do dodawania lub edytowania autora
        [HttpPost]
        public async Task<IActionResult> Upsert(int? id, AuthorModel author)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    // Sprawdzanie, czy autor o podanej nazwie już istnieje
                    var foundItem = await _context.Authors.FirstOrDefaultAsync(u => u.Name == author.Name);

                    if (foundItem != null)
                    {
                        TempData["AlertMessage"] = "'" + author.Name + "' exists in the list! It hasn't been added to the list";
                        return RedirectToAction("Index");
                    }

                    // Dodawanie nowego autora do bazy danych
                    await _context.Authors.AddAsync(author);
                    TempData["AlertMessage"] = "'" + author.Name + "' has been added to the list successfully";
                }
                else
                {
                    // Sprawdzanie, czy nowa nazwa autora już istnieje, ale wykluczając bieżącego autora
                    var foundItem = await _context.Authors.FirstOrDefaultAsync(u => u.Name == author.Name && u.ID != id); 
                    if (foundItem != null) 
                    { 
                        TempData["AlertMessage"] = "'" + author.Name + "' exists in the list! It hasn't been changed"; 
                        return RedirectToAction("Index"); 
                    }

                    // Aktualizacja nazwy autora
                    var items = await _context.Authors.FirstOrDefaultAsync(u => u.ID == id);
                    items.Name = author.Name;
                    TempData["AlertMessage"] = "'" + author.Name + "' has been edited";
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // GET: Akcja Delete do usuwania autora
        public IActionResult Delete(int id)
        {
            var item = _context.Authors.FirstOrDefault(u => u.ID == id);
            return View(item);
        }

        // POST: Akcja Delete do usuwania autora
        [HttpPost]
        public async Task<IActionResult> Delete(AuthorModel author)
        {
            var item = _context.Authors.FirstOrDefault(u => u.ID == author.ID);

            TempData["AlertMessage"] = "'" + item.Name + "' has been deleted successfully";
            _context.Authors.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
