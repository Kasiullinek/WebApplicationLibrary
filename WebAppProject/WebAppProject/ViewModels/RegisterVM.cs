using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    // Model widoku dla formularza rejestracji
    public class RegisterVM
    {
        // Model użytkownika
        public UserModel? userModel { get; set; }

        // Numer telefonu użytkownika
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        // Adres email użytkownika
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        // Hasło użytkownika
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        // Potwierdzenie hasła użytkownika
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords Do Not Match!")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}
