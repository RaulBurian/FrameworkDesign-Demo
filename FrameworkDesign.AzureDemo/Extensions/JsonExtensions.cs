using Newtonsoft.Json;

namespace FrameworkDesign.AzureDemo.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJsonString(this object self)
        {
            return JsonConvert.SerializeObject(self);
        }
    }
}