using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAppProject.Models
{
    public class PublisherModel
    {
        [Key]
        [Description("Primary key for the publisher item")]
        public int ID { get; set; }

        [Description("Name of the publisher")]
        [Required(ErrorMessage = "Publisher name is required.")]
        [RegularExpression(@"^[A-ZĄĆĘŁŃÓŚŹŻa-ząćęłńóśźż0-9&\-'.\s]+$",
            ErrorMessage = "Publisher name must start with an uppercase letter and can include letters, digits, spaces, and symbols like &, -, ', or ..")]
        [StringLength(50, ErrorMessage = "Length cannot exceed 50 characters.")]
        public string Name { get; set; } = "";
    }
}
