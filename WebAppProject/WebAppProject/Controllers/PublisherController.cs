using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    [Authorize(Roles ="admin")]
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor kontrolera PublisherController
        public PublisherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Akcja Index zwracająca listę wydawców
        public IActionResult Index()
        {
            var items = _context.Publishers.OrderBy(p => p.Name).ToList();

            return View(items);
        }

        // GET: Akcja Upsert do dodawania lub edytowania wydawcy
        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                // Tworzenie nowego obiektu PublisherModel
                PublisherModel publisher = new PublisherModel();
                return View(publisher);
            }
            else
            {
                // Pobieranie wydawcy z bazy danych na podstawie ID
                var items = _context.Publishers.FirstOrDefault(u => u.ID == id);
                return View(items);
            }
        }

        // POST: Akcja Upsert do dodawania lub edytowania wydawcy
        [HttpPost]
        public async Task<IActionResult> Upsert(int? id, PublisherModel publisher)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    // Sprawdzanie, czy wydawca o podanej nazwie już istnieje
                    var foundItem = await _context.Publishers.FirstOrDefaultAsync(u => u.Name == publisher.Name);

                    if (foundItem != null)
                    {
                        TempData["AlertMessage"] = "'" + publisher.Name + "' exists in the list! It hasn't been added to the list";
                        return RedirectToAction("Index");
                    }

                    // Dodawanie nowego wydawcy do bazy danych
                    await _context.Publishers.AddAsync(publisher);
                    TempData["AlertMessage"] = "'" + publisher.Name + "' has been added to the list successfully";
                }
                else
                {
                    // Sprawdzanie, czy nowa nazwa wydawcy już istnieje, ale wykluczając bieżącego wydawcę
                    var foundItem = await _context.Publishers.FirstOrDefaultAsync(u => u.Name == publisher.Name && u.ID != id);
                    if (foundItem != null)
                    {
                        TempData["AlertMessage"] = "'" + publisher.Name + "' exists in the list! It hasn't been changed";
                        return RedirectToAction("Index");
                    }

                    // Aktualizacja nazwy wydawcy
                    var items = await _context.Publishers.FirstOrDefaultAsync(u => u.ID == id);
                    items.Name = publisher.Name;
                    TempData["AlertMessage"] = "'" + publisher.Name + "' has been edited";
                }

                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }

        //GET: Akcja Delete do usuwania wydawcy
        public IActionResult Delete(int id)
        {
            var item = _context.Publishers.FirstOrDefault(u => u.ID == id);
            return View(item);
        }

        //POST: Akcja Delete do usuwania wydawcy
        [HttpPost]
        public async Task<IActionResult> Delete(PublisherModel publisher)
        {
            var item = _context.Publishers.FirstOrDefault(u => u.ID == publisher.ID);

            TempData["AlertMessage"] = "'" + item.Name + "' has been deleted successfully";
            _context.Publishers.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
