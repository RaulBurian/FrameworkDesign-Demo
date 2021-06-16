namespace FrameworkDesign.AzureDemo.Models
{
    public class EmailRequest
    {
        public string Recipient { get; set; }

        public string FileUrl { get; set; }

        public string EncodingKey { get; set; }
    }
}