using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class CategoryModel
    {
        [Key]
        [Description("Primary key for the category item")]
        public int ID { get; set; }

        [Description("Name of age category of the book")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Category must start with an uppercase letter and contain only letters and spaces.")]
        [StringLength(20, ErrorMessage = "Length cannot go above 20.")]
        public string Name { get; set; } = "";
    }
}
