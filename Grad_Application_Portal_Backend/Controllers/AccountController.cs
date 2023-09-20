using Grad_Application_Portal_Backend.Data;
using Grad_Application_Portal_Backend.Models;
using Grad_Application_Portal_Backend.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace Grad_Application_Portal_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private GradPortalDBContext _context { get; set; }
        private IConfiguration _Config;

        public AccountController(GradPortalDBContext ctx,IConfiguration configuration)
        {
            _context = ctx;
            _Config = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login(LoginViewModel login)
        {

            if (ModelState.IsValid)
            {
                var user = Authenticate(login);

                if (user != null)
                {
                    var Token = GenerateToken(user);

                    return new JsonResult(Token);
                }
                else
                    return new JsonResult("Invalid password/email");
            }
            else
                return new JsonResult("Invalid password/email");
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.GivenName,user.EmailAddress),
            };

            var token = new JwtSecurityToken(_Config["Jwt:Issuer"], _Config["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private User Authenticate(LoginViewModel login)
        {
            #region HASH and salt Passoword encryption

            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            string encryptedPassword = PasswordEncryptor.EncryptPassword(login.Password, salt);

            #endregion

            //Replace login.Password with encryptedPassword
            var results = _context.users.Where(u => u.EmailAddress == login.Email && u.Password == encryptedPassword);

            if (results != null)
            {
                User user = new User();

                foreach (var u in results)
                {
                    user.UserID = u.UserID;
                    user.EmailAddress = u.EmailAddress;
                }

                return user;

            }
            else
                return null;
        }
    }
}
