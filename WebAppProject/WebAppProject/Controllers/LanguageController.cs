using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class LanguageController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor kontrolera LanguageController
        public LanguageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Akcja Index zwracająca listę języków
        public IActionResult Index()
        {
            var items = _context.Languages.OrderBy(l => l.Name).ToList();

            return View(items);
        }

        //GET: Akcja Upsert do dodawania lub edytowania języka
        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                // Tworzenie nowego obiektu LanguageModel
                LanguageModel language = new LanguageModel();
                return View(language);
            }
            else
            {
                // Pobieranie języka z bazy danych na podstawie ID
                var items = _context.Languages.FirstOrDefault(u => u.ID == id);
                return View(items);
            }
        }

        //POST: Akcja Upsert do dodawania lub edytowania języka
        [HttpPost]
        public async Task<IActionResult> Upsert(int? id, LanguageModel language)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    // Sprawdzanie, czy języka o podanej nazwie już istnieje
                    var foundItem = await _context.Languages.FirstOrDefaultAsync(u => u.Name == language.Name);

                    if (foundItem != null)
                    {
                        TempData["AlertMessage"] = "'" + language.Name + "' exists in the list! It hasn't been added to the list";
                        return RedirectToAction("Index");
                    }

                    // Dodawanie nowego języka do bazy danych
                    await _context.Languages.AddAsync(language);
                    TempData["AlertMessage"] = "'" + language.Name + "' has been added to the list successfully";
                }
                else
                {
                    // Sprawdzanie, czy nowa nazwa języka już istnieje, ale wykluczając bieżący język
                    var foundItem = await _context.Languages.FirstOrDefaultAsync(u => u.Name == language.Name && u.ID != id);
                    if (foundItem != null)
                    {
                        TempData["AlertMessage"] = "'" + language.Name + "' exists in the list! It hasn't been changed";
                        return RedirectToAction("Index");
                    }

                    // Aktualizacja nazwy języka
                    var items = await _context.Languages.FirstOrDefaultAsync(u => u.ID == id);
                    items.Name = language.Name;
                    TempData["AlertMessage"] = "'" + language.Name + "' has been edited";
                }

                await _context.SaveChangesAsync();
            }

            
            return RedirectToAction("Index");
        }

        //GET: Akcja Delete do usuwania języka
        public IActionResult Delete(int id)
        {
            var item = _context.Languages.FirstOrDefault(u => u.ID == id);
            return View(item);
        }

        //POST: Akcja Delete do usuwania języka
        [HttpPost]
        public async Task<IActionResult> Delete(LanguageModel language)
        {
            var item = _context.Languages.FirstOrDefault(u => u.ID == language.ID);

            TempData["AlertMessage"] = "'" + item.Name + "' has been deleted successfully";
            _context.Languages.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
