using E_CommerceAPI.Application.Abstractions.Storage.GCP;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services.Storage.GCP
{
    public class GCPStorage :Storage, IGCPStorage
    {

        private readonly GoogleCredential _googleCredential;
        private readonly IConfiguration _configuration;
        public GCPStorage(IConfiguration configuration)
        {
            _googleCredential = GoogleCredential.FromFile(configuration["Storage:GCPStorageAuthFile"]);
            _configuration = configuration;
        }


        public async Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            // Credentialım'dan ban bir client olusturucak
            using var storageClient = StorageClient.Create(_googleCredential);
            fileName = pathOrContainerName + "/" + fileName;

            await storageClient.DeleteObjectAsync(_configuration["Storage:GoogleCloudStorageBucketName"], fileName);

        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            // Credentialım'dan ban bir client olusturucak
            using var storageClient =  StorageClient.Create(_googleCredential);
            List<string> files = new List<string>();
            files = storageClient.ListObjects(_configuration["Storage:GoogleCloudStorageBucketName"], pathOrContainerName).Select(o => o.Name).ToList();

            return files;

        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            // Credentialım'dan ban bir client olusturucak
            using var storageClient = StorageClient.Create(_googleCredential);
            fileName = pathOrContainerName+"/" + fileName;
            return storageClient.ListObjects(_configuration["Storage:GoogleCloudStorageBucketName"]).Any(o => o.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            // Credentialım'dan ban bir client olusturucak
            using var storageClient = StorageClient.Create(_googleCredential);

            List<(string fileName, string pathOrContainerName)> datas = new();

            foreach (IFormFile file in files)
            {
                string newFileName = await FileRenameAsync(pathOrContainerName, file.Name, HasFile);
                newFileName = pathOrContainerName + "/" + newFileName;

                var uploadedFile = await storageClient.UploadObjectAsync(
                        _configuration["Storage:GoogleCloudStorageBucketName"],
                        newFileName,
                        file.ContentType,
                        file.OpenReadStream()
                    );

                datas.Add((file.Name, pathOrContainerName));
            }
            return datas;
        }
    }
}
