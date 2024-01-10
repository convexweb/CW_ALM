using System.Security.Cryptography;

namespace CW_ALM.Domain.Core
{
    public class SecurityCipherStrings
    {
        public static string ConvertString2MD5(string input)
        {
            MD5 md5String = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5String.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
