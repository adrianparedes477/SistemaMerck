using System;
using System.Security.Cryptography;
using System.Text;

namespace SistemaMerck.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password, out string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltBytes = GenerateSalt();
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Combina la contraseña con la sal
                byte[] combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

                // Calcula el hash de la combinación de la contraseña y la sal
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);

                salt = Convert.ToBase64String(saltBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[32];
                rng.GetBytes(saltBytes);
                return saltBytes;
            }
        }
    }

}
