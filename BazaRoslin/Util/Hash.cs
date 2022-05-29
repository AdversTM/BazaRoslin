using System.Security.Cryptography;
using System.Text;

namespace BazaRoslin.Util {
    public static class Hash {
        public static string CreateMD5(string text) {
            using var md5 = MD5.Create();
            var input = Encoding.ASCII.GetBytes(text);
            var hash = md5.ComputeHash(input);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));
            return sb.ToString();
        }
    }
}