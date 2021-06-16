using System;
using System.Text;
using System.Threading.Tasks;
using FrameworkDesign.AzureDemo.Extensions;
using FrameworkDesign.AzureDemo.Models;
using FrameworkDesign.AzureDemo.Services.Interfaces;
using FrameworkDesign.AzureDemo.Utils;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace FrameworkDesign.AzureDemo.Functions
{
    public class EncodeFile
    {
        private const string ContainerName = "files";
        private readonly IBlobService _blobService;

        public EncodeFile(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [FunctionName(nameof(EncodeFile))]
        public async Task RunAsync([ServiceBusTrigger("%TopicName%", "%EncodingSubscription%", Connection = "ServiceBusConnectionString")]
            Message message)
        {
            var messageBody = JsonConvert.DeserializeObject<FileEncodedRequest>(Encoding.UTF8.GetString(message.Body));
            var newIdentifier = Guid.NewGuid().ToString();
            var encodedMessageBody = EncodeMessage(messageBody.Content, newIdentifier);

            var decodedMessageBody = StringCipher.Decipher(encodedMessageBody, Convert.ToBase64String(newIdentifier.ToByteArray()));

            await _blobService.UploadContentBlobAsync(ContainerName, encodedMessageBody, $"{messageBody.Email}={newIdentifier}.txt");
        }

        private static string EncodeMessage(string messageBody, string identifier)
        {
            var base64Identifier = Convert.ToBase64String(identifier.ToByteArray());
            return StringCipher.Encipher(messageBody, base64Identifier);
        }
    }
}