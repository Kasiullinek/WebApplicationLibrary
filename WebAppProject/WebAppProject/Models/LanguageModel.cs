using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAppProject.Models
{
    public class LanguageModel
    {
        [Key]
        [Description("Primary key for the language item")]
        public int ID { get; set; }

        [Description("Name of language of the book")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Language must start with an uppercase letter and contain only letters and spaces.")]
        [StringLength(20, ErrorMessage = "Length cannot go above 20.")]
        public string Name { get; set; } = "";
    }
}
