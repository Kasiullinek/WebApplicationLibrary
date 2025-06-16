using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppProject.Models
{
    public class CartModel
    {
        [Key]
        [Description("Primary key for the cart item")]
        public int ID { get; set; }

        [Description("Identifier of the book")]
        public int? BookID { get; set; }
        [ForeignKey("BookID")]

        [Description("Details of the book associated with the cart item")]
        public BookModel? book { get; set; }

        [Description("Identifier of the user")]
        public string? UserID { get; set; }
        [ForeignKey("UserID")]

        [Description("Details of the user associated with the cart item")]
        public UserModel? user { get; set; }
    }
}
