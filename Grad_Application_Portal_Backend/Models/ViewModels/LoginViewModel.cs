using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Grad_Application_Portal_Backend.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please enter email address.")]
        [EmailAddress(ErrorMessage = "Incorrect email address format.")]
        //[RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Incorrect email address format.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter password.")]
        public  string Password { get; set; }
    }
}
