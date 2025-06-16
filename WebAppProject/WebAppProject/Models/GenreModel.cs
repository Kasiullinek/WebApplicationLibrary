using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAppProject.Models
{
    public class GenreModel
    {
        [Key]
        [Description("Primary key for the genre item")]
        public int ID { get; set; }

        [Description("Name of genre of the book")]
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Genre must start with an uppercase letter and contain only letters and spaces.")]
        [StringLength(30, ErrorMessage = "Length cannot go above 20.")]
        public string Name { get; set; } = "";
    }
}
