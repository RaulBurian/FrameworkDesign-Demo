using System.IO;
using System.Threading.Tasks;
using FrameworkDesign.AzureDemo.Extensions;
using FrameworkDesign.AzureDemo.Models;
using FrameworkDesign.AzureDemo.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace FrameworkDesign.AzureDemo.Functions
{
    public class SaveFile
    {
        [FunctionName("ReceiveMessage")]
        public async Task<IActionResult> ReceiveMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "text")]
            HttpRequest req,
            [ServiceBus("%TopicName%", Connection = "ServiceBusConnectionString")]
            MessageSender messageSender)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            var fileEncodeRequest = JsonConvert.DeserializeObject<FileEncodedRequest>(content);

            await messageSender.SendAsync(new Message(fileEncodeRequest.ToJsonByteArray()));

            return new OkResult();
        }

        [FunctionName("DecodeFile")]
        public async Task<IActionResult> DecodeFile([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "decode")]
            HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            var decodeRequest = JsonConvert.DeserializeObject<DecodeRequest>(content);

            return new OkObjectResult(new DecodeResponse
            {
                DecodedText = StringCipher.Decipher(decodeRequest.Content, decodeRequest.EncodingKey)
            });
        }
    }
}