using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Data;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor kontrolera CategoryController
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Akcja Index zwracająca listę kategorii
        public IActionResult Index()
        {
            var items = _context.Categories.OrderBy(c => c.Name).ToList();

            return View(items);
        }

        //GET: Akcja Upsert do dodawania lub edytowania kategorii
        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                // Tworzenie nowego obiektu CategoryModel
                CategoryModel category = new CategoryModel();
                return View(category);
            }
            else
            {
                // Pobieranie kategorii z bazy danych na podstawie ID
                var items = _context.Categories.FirstOrDefault(u => u.ID == id);
                return View(items);
            }
        }

        // POST: Akcja Upsert do dodawania lub edytowania kategorii
        [HttpPost]
        public async Task<IActionResult> Upsert(int? id, CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    // Sprawdzanie, czy kategoria o podanej nazwie już istnieje
                    var foundItem = await _context.Categories.FirstOrDefaultAsync(u => u.Name == category.Name);

                    if (foundItem != null)
                    {
                        TempData["AlertMessage"] = "'" + category.Name + "' exists in the list! It hasn't been added to the list";
                        return RedirectToAction("Index");
                    }

                    // Dodawanie nowej kategorii do bazy danych
                    await _context.Categories.AddAsync(category);
                    TempData["AlertMessage"] = "'" + category.Name + "' has been added to the list successfully";
                }
                else
                {
                    // Sprawdzanie, czy nowa nazwa kategorii już istnieje, ale wykluczając bieżącą kategorię
                    var foundItem = await _context.Categories.FirstOrDefaultAsync(u => u.Name == category.Name && u.ID != id);
                    if (foundItem != null)
                    {
                        TempData["AlertMessage"] = "'" + category.Name + "' exists in the list! It hasn't been changed";
                        return RedirectToAction("Index");
                    }

                    // Aktualizacja nazwy kategorii
                    var items = await _context.Categories.FirstOrDefaultAsync(u => u.ID == id);
                    items.Name = category.Name;
                    TempData["AlertMessage"] = "'" + category.Name + "' has been edited";
                }

                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }

        // GET: Akcja Delete do usuwania kategorii
        public IActionResult Delete(int id)
        {
            var item = _context.Categories.FirstOrDefault(u => u.ID == id);
            return View(item);
        }

        // POST: Akcja Delete do usuwania kategorii
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryModel category)
        {
            var item = _context.Categories.FirstOrDefault(u => u.ID == category.ID);

            TempData["AlertMessage"] = "'" + item.Name + "' has been deleted successfully";
            _context.Categories.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
