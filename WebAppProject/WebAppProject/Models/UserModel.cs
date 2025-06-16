using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebAppProject.Models
{
    public class UserModel : IdentityUser
    {
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Description("First name of the user")]
        public string FirstName { get; set; } = "";


        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Description("Last name of the user")]
        public string LastName { get; set; } = "";


        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [CustomValidation(typeof(UserModel), nameof(ValidateBirthDate))]
        [Description("Date of birth of the user")]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "PESEL is required.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "PESEL must be exactly 11 characters.")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "PESEL must contain only digits.")]
        [Description("PESEL (national identification number) of the user")]
        public string PESEL { get; set; } = "";


        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        [Description("Address of the user")]
        public string Address { get; set; } = "";


        // Validation method for date of birth
        public static ValidationResult ValidateBirthDate(DateTime date)
        {
            if (date > DateTime.Now)
            {
                return new ValidationResult("Birth date cannot be in the future.");
            }

            return ValidationResult.Success;
        }
    }
}
