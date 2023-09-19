using System;
using System.Security.Cryptography;
using System.Text;

namespace Grad_Application_Portal_Backend.Models
{  public class PasswordEncryptor
    {
        public static string EncryptPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // 32 bytes for a 256-bit key
                return Convert.ToBase64String(hashBytes);
            }
        }
    }

}

