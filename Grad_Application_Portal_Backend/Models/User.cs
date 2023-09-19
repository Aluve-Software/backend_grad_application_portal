using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Grad_Application_Portal_Backend.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required(ErrorMessage ="Please enter email password.")]
        [EmailAddress(ErrorMessage ="Incorrect email address format.")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Incorrect email address format.")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage ="Please enter password.")]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Please confirm password.")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
