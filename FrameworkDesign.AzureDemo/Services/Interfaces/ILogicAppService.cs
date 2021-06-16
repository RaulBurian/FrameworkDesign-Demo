using System.Net.Http;
using System.Threading.Tasks;
using FrameworkDesign.AzureDemo.Models;

namespace FrameworkDesign.AzureDemo.Services.Interfaces
{
    public interface ILogicAppService
    {
        Task<HttpResponseMessage> SendNotification(NotificationRequest notificationRequest);

        Task<HttpResponseMessage> SendEncodedFile(EmailRequest emailRequest);
    }
}