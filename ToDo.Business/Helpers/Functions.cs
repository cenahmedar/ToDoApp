using System;
using System.Security.Cryptography;
using System.Text;

namespace ToDo.Business.Helpers
{
    public class Functions
    {
        public static string MD5(string text)
        {
            byte[] result = new byte[text.Length];
            MD5 md = new MD5CryptoServiceProvider();
            UTF8Encoding encode = new UTF8Encoding();
            result = md.ComputeHash(encode.GetBytes(text));
            return BitConverter.ToString(result).Replace("-", "");

        }
    }
}
