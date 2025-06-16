using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    // Model widoku dla strony głównej
    public class HomeVM
    {
        // Lista książek wyświetlanych na stronie głównej
        public IEnumerable<BookModel>? BookList { get; set; }

        // Tytuł książki do wyszukiwania
        public string? SearchTitle { get; set; }

        // Paginacja
        public int? CurrentPage { get; set; }
        public int? TotalPages { get; set; }
    }
}
