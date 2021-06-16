using System.Linq;

namespace FrameworkDesign.AzureDemo.Utils
{
    public static class StringCipher
    {
        public static string Encipher(string input, string encodingKey)
        {
            var key = ComputeKey(encodingKey);
            return ComputeCipher(input, key % 26);
        }

        public static string Decipher(string input, string encodingKey)
        {
            var key = ComputeKey(encodingKey);
            return ComputeCipher(input, 26 - key % 26);
        }

        private static char Cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {
                return ch;
            }

            var d = char.IsUpper(ch) ? 'A' : 'a';
            return (char) ((((ch + key) - d) % 26) + d);
        }

        private static int ComputeKey(string key)
        {
            return key.Aggregate(0, (current, ch) => current += ch);
        }

        private static string ComputeCipher(string input, int key)
        {
            return input.Aggregate(string.Empty, (current, ch) => current + Cipher(ch, key));
        }
    }
}