using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace FrameworkDesign.AzureDemo.Extensions
{
    public static class ByteArrayExtensions
    {
        public static byte[] ToJsonByteArray(this object source)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(source));
        }

        public static byte[] ToByteArray(this string source)
        {
            return Encoding.UTF8.GetBytes(source);
        }

        public static byte[] ToByteArray(this Stream input)
        {
            var ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }
}