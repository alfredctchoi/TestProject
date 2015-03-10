using System.Security.Cryptography;
using System.Text;

namespace TestProject.Service
{
    public static class PasswordUtility
    {
        public static string Encrypt(string password)
        {
            string passwordHashed;
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

                var sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }

                passwordHashed = sb.ToString();
            }

            return passwordHashed;
        }
    }
}
