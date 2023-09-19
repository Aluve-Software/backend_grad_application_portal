using Grad_Application_Portal_Backend.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Grad_Application_Portal_Backend.Models
{
    public class Register
    {
        [EmailValidation(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [PasswordValidation(ErrorMessage = "Password should contain at least one uppercase letter, one lowercase letter, and one digit. It should be between 8 and 50 characters long.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }

}
