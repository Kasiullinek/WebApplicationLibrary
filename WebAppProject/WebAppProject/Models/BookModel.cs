using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAppProject.Models
{
    public class BookModel
    {
        [Key]
        [Description("Primary key for the book")]
        public int ID { get; set; }

        [Description("Title of the book")]
        [Required]
        public string Title { get; set; }


        [Description("Author's name of the book")]
        public int AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        public AuthorModel? author { get; set; }


        [Description("Description of the book")]
        [MaxLength(500)]
        public string Description { get; set; }


        [Description("Publication's date of the book")]
        [Required, DataType(DataType.Date), Display(Name = "Publication Date")]
        [CustomValidation(typeof(BookModel), nameof(ValidatePublishDate))]
        public DateTime PublishDate { get; set; }


        [Description("Publisher of the book")]
        public int PublisherID { get; set; }
        [ForeignKey("PublisherID")]
        public PublisherModel? publisher { get; set; }


        [Description("Category of the book")]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public CategoryModel? category { get; set; }


        [Description("Genre of the book")]
        public int GenreID { get; set; }
        [ForeignKey("GenreID")]
        public GenreModel? genre { get; set; }


        [Description("Language of the book")]
        public int LanguageID { get; set; }
        [ForeignKey("LanguageID")]
        public LanguageModel? language { get; set; }


        [Description("ISBN numer of the book")]
        [Required, MaxLength(13), RegularExpression(@"^\d{10}(\d{3})?$", ErrorMessage = "ISBN must be 10 or 13 digits.")]
        public string ISBN { get; set; }


        [Description("Cover's picture of the book")]
        [Display(Name = "Image URL")]
        public string? ImgUrl { get; set; }


        // Validation method for publish date
        public static ValidationResult ValidatePublishDate(DateTime date, ValidationContext context)
        {
            return date <= DateTime.Now ? ValidationResult.Success : new ValidationResult("Publish date cannot be in the future.");
        }
    }
}
