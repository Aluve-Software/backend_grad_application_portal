
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Grad_Application_Portal_Backend.Models.ViewModels
{ 
    public class EmailValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string email = value as string;

            if (string.IsNullOrEmpty(email))
                return false;

            
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }

    public class PasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string password = value as string;

            if (string.IsNullOrEmpty(password))
                return false;

       
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,50}$");
        }
    }




}
