using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FrameworkDesign.AzureDemo.Extensions;
using FrameworkDesign.AzureDemo.Models;
using FrameworkDesign.AzureDemo.Services.Interfaces;
using FrameworkDesign.AzureDemo.Settings;

namespace FrameworkDesign.AzureDemo.Services
{
    public class LogicAppService : ILogicAppService
    {
        private readonly HttpClient _httpClient;
        private readonly LogicAppSettings _logicAppSettings;

        public LogicAppService(HttpClient httpClient, LogicAppSettings logicAppSettings)
        {
            _httpClient = httpClient;
            _logicAppSettings = logicAppSettings;
        }

        public async Task<HttpResponseMessage> SendNotification(NotificationRequest notificationRequest)
        {
            var logicAppRequest = new LogicAppRequest
            {
                Notification = true,
                Recipient = notificationRequest.Recipient
            };

            return await SendRequest(logicAppRequest);
        }

        public async Task<HttpResponseMessage> SendEncodedFile(EmailRequest emailRequest)
        {
            var logicAppRequest = new LogicAppRequest
            {
                Notification = false,
                Recipient = emailRequest.Recipient,
                FileUrl = emailRequest.FileUrl,
                EncodingKey = emailRequest.EncodingKey
            };

            return await SendRequest(logicAppRequest);
        }

        private async Task<HttpResponseMessage> SendRequest(LogicAppRequest requestBody)
        {
            var content = new StringContent(requestBody.ToJsonString(), Encoding.UTF8, "application/json");
            var httRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_logicAppSettings.Url),
                Content = content
            };

            return await _httpClient.SendAsync(httRequestMessage);
        }
    }
}