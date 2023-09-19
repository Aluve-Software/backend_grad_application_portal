using Grad_Application_Portal_Backend.Data;
using Grad_Application_Portal_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System;

namespace Grad_Application_Portal_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly GradPortalDBContext gradPortalDBContext;

        public RegisterController(GradPortalDBContext gradPortalDBContext)
        {
            this.gradPortalDBContext = gradPortalDBContext;
        }
        
            [HttpPost]
            public IActionResult Register(Register model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "Invalid registration data.", Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
                }

              
                byte[] saltBytes = new byte[16];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(saltBytes);
                }
                string salt = Convert.ToBase64String(saltBytes);

            
                string encryptedPassword = PasswordEncryptor.EncryptPassword(model.Password, salt);

            

                return Ok(new { Message = "Registration successful.", EncryptedPassword = encryptedPassword });
            }
    }

}


