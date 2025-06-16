using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    // Model widoku dla generowania raportów
    public class ReportVM
    {
        // Lista zamówień do wyświetlenia w raporcie
        public IEnumerable<OrderModel>? OrderList { get; set; }

        // Lista opcji dostępnych do filtrowania
        public List<CategoryModel>? CategoriesList { get; set; }
        public List<LanguageModel>? LanguagesList { get; set; }
        public List<AuthorModel>? AuthorsList { get; set; }
        public List<PublisherModel>? PublishersList { get; set; }
        public List<GenreModel>? GenresList { get; set; }

        // Wybrane filtry
        public List<int>? SelectedCategories { get; set; }
        public List<int>? SelectedGenres { get; set; }
        public List<int>? SelectedLanguages { get; set; }
        public List<int>? SelectedPublishers { get; set; }
        public List<int>? SelectedAuthors { get; set; }

        // Filtry daty
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }

        // Filtr nazwy użytkownika
        public string? SearchUserName { get; set; }

        // Paginacja
        public int? CurrentPage { get; set; }
        public int? TotalPages { get; set; }
    }
}
