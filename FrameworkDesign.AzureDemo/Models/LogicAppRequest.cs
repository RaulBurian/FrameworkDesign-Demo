namespace FrameworkDesign.AzureDemo.Models
{
    public class LogicAppRequest
    {
        public string EncodingKey { get; set; }

        public string FileUrl { get; set; }

        public bool Notification { get; set; }

        public string Recipient { get; set; }
    }
}