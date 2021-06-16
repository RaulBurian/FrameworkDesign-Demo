using System.Text;
using System.Threading.Tasks;
using FrameworkDesign.AzureDemo.Models;
using FrameworkDesign.AzureDemo.Services.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace FrameworkDesign.AzureDemo.Functions
{
    public class SendNotification
    {
        private readonly ILogicAppService _logicAppService;

        public SendNotification(ILogicAppService logicAppService)
        {
            _logicAppService = logicAppService;
        }

        [FunctionName(nameof(SendNotification))]
        public async Task RunAsync([ServiceBusTrigger("%TopicName%", "%NotificationSubscription%", Connection = "ServiceBusConnectionString")]
            Message message, MessageReceiver messageReceiver)
        {
            var messageRequest = JsonConvert.DeserializeObject<FileEncodedRequest>(Encoding.UTF8.GetString(message.Body));

            var response = await _logicAppService.SendNotification(new NotificationRequest { Recipient = messageRequest.Email });

            if (!response.IsSuccessStatusCode)
            {
                await messageReceiver.DeadLetterAsync(message.SystemProperties.LockToken);
            }
        }
    }
}