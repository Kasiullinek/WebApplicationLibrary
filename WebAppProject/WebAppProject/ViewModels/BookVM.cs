using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    // Model widoku dla zarządzania danymi książki
    public class BookVM
    {
        // Obiekt reprezentujący książkę
        public BookModel? Books { get; set; }

        // Lista opcji do wyświetlenia w rozwijanym menu
        public IEnumerable<SelectListItem>? CategoriesList { get; set; }
        public IEnumerable<SelectListItem>? LanguagesList { get; set; }
        public IEnumerable<SelectListItem>? AuthorsList { get; set; }
        public IEnumerable<SelectListItem>? PublishersList { get; set; }
        public IEnumerable<SelectListItem>? GenresList { get; set; }

        // Plik obrazu związany z książką
        public IFormFile? ImageFile { get; set; }
    }
}
