using System;
using Azure.Storage.Blobs;
using FrameworkDesign.AzureDemo;
using FrameworkDesign.AzureDemo.Services;
using FrameworkDesign.AzureDemo.Services.Interfaces;
using FrameworkDesign.AzureDemo.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Startup))]

namespace FrameworkDesign.AzureDemo
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IBlobService, BlobService>();
            builder.Services.AddSingleton<ILogicAppService, LogicAppService>();
            builder.Services.AddHttpClient();
            AddBlobService(builder);
            AddLogicAppSettings(builder);
        }

        private static void AddBlobService(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("StorageAccountConnectionString", EnvironmentVariableTarget.Process);
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                builder.Services.AddSingleton(_ => new BlobServiceClient(connectionString));
            }
        }

        private static void AddLogicAppSettings(IFunctionsHostBuilder builder)
        {
            var logicAppSettings = new LogicAppSettings
            {
                Url = Environment.GetEnvironmentVariable("LogicAppUrl", EnvironmentVariableTarget.Process)
            };
            builder.Services.AddSingleton(logicAppSettings);
        }
    }
}