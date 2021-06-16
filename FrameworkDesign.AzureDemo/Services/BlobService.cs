using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FrameworkDesign.AzureDemo.Extensions;
using FrameworkDesign.AzureDemo.Services.Interfaces;

namespace FrameworkDesign.AzureDemo.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService()
        {
        }

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task UploadContentBlobAsync(string containerName, string content, string fileName)
        {
            var blobClient = GetBlobClient(fileName, containerName);
            await using var memoryStream = new MemoryStream(content.ToByteArray());
            await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = "text/plain" });
        }

        private BlobClient GetBlobClient(string blobName, string containerName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blob = container.GetBlobClient(blobName);
            return blob;
        }
    }
}