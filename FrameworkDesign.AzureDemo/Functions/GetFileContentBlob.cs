using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FrameworkDesign.AzureDemo.Extensions;
using FrameworkDesign.AzureDemo.Models;
using FrameworkDesign.AzureDemo.Services.Interfaces;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;

namespace FrameworkDesign.AzureDemo.Functions
{
    public class GetFileContentBlob
    {
        private readonly ILogicAppService _logicAppService;

        public GetFileContentBlob(ILogicAppService logicAppService)
        {
            _logicAppService = logicAppService;
        }

        [FunctionName("GetFileContentBlob")]
        public async Task RunAsync([BlobTrigger("files/{name}", Connection = "StorageAccountConnectionString")]
            CloudBlockBlob myBlob, string name)
        {
            Thread.Sleep(10000);
            var nameSplit = name.Split('=');
            var emailRequest = new EmailRequest
            {
                Recipient = nameSplit[0],
                EncodingKey = ComputeEncodingKey(nameSplit),
                FileUrl = myBlob.Uri.AbsoluteUri
            };
            await _logicAppService.SendEncodedFile(emailRequest);
        }

        private static string ComputeEncodingKey(IReadOnlyList<string> nameSplit)
        {
            return Convert.ToBase64String(nameSplit[1].Split('.')[0].ToByteArray());
        }
    }
}