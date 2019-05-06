using System;
using System.Text;

namespace RestaurantMng.Core.Common
{
    public class Encryption
    {
        public const string SECRET_KEY = "abcxyz";

        /// <summary>
        /// Encode base64
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string Base64Encode(string text)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Mã hóa MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Băm input + secret key
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HashMD5(string input, string salt = "")
        {
            string base64String1 = Base64Encode(input);
            string base64String2 = Base64Encode(salt);
            return CreateMD5(string.Concat(base64String1, salt, SECRET_KEY));
        }
    }
}
