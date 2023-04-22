using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;

namespace ASS2
{
    public static class GetImage
    {
        [FunctionName("GetImage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log) {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var credential = new ChainedTokenCredential(
                new ManagedIdentityCredential(string.IsNullOrEmpty(config["UserAssignedIdentity"])
                    ? null 
                    : config["UserAssignedIdentity"]),
                new DefaultAzureCredential());

            var blobServiceClient = new BlobServiceClient(new Uri(config["StorageAccountName"]), credential);
            var containerClient = blobServiceClient.GetBlobContainerClient(config["ContainerName"]);
            var blob = containerClient.GetBlobClient(config["ImageName"]);
            var picture = blob.DownloadContent();

            return new FileContentResult(picture.Value.Content.ToArray(), "image/png");
        }
    }
}
