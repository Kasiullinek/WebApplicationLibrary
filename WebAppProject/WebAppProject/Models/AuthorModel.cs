using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAppProject.Models
{
    public class AuthorModel
    {
        [Key]
        [Description("Primary key for the author item")]
        public int ID { get; set; }

        [Description("Author's name of the book")]
        [Required(ErrorMessage = "Author's name is required.")]
        [RegularExpression(@"^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻ\s\-\.'']*$",
            ErrorMessage = "Name of the Author must start with an uppercase letter and can contain only letters, spaces, hyphens, apostrophes, or periods.")]
        [StringLength(50, ErrorMessage = "Length cannot exceed 50 characters.")]
        public string Name { get; set; } = "";
    }
}
