using System.Security.Cryptography;
using System.Text;

namespace Challenge.Api.Services
{
    public static class ChallengeHash
    {
        /// <summary>
        /// تبدیل رشته به کد هش شده MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToMD5(this string str)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(str);
            data = provider.ComputeHash(data);
            return Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// تبدیل به هش 256
        /// </summary>
        public static string ConvertToSHA256(this string str)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(str);
            data = provider.ComputeHash(data);
            return Encoding.UTF8.GetString(data);
        }
    }
}