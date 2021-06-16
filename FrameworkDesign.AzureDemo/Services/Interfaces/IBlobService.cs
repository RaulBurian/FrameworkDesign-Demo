using System.Threading.Tasks;

namespace FrameworkDesign.AzureDemo.Services.Interfaces
{
    public interface IBlobService
    {
        Task UploadContentBlobAsync(string containerName, string content, string fileName);
    }
}