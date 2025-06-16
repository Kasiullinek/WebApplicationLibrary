using System.ComponentModel.DataAnnotations;

namespace WebAppProject.ViewModels
{
    // Model widoku dla formularza logowania
    public class LoginVM
    {
        // Adres email użytkownika
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        // Hasło użytkownika
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
