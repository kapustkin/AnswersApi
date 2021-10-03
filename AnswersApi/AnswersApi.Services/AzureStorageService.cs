using AnswersApi.Common.Interfaces;
using AnswersApi.Common.Models.Base;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AnswersApi.Services
{
    /// <summary>
    /// Реализация сервиса по работе с хранилищем
    /// </summary>
    public class AzureStorageService : IStorageService
    {
        private readonly string _storageConnectionString;
        private readonly ILogger<AzureStorageService> _logger;

        public AzureStorageService(IConfiguration configuration, ILogger<AzureStorageService> logger)
        {
            _storageConnectionString = configuration.GetConnectionString("AzureStorageConnection") ?? throw new ArgumentNullException(nameof(configuration), "Отсутствует конфигурация AzureStorageConnection");
            _logger = logger;
        }

        public async Task<DataResult<string>> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            try
            {
                //TODO вынести в настройки file-container
                var container = new BlobContainerClient(_storageConnectionString, "file-container");
                var createResponse = await container.CreateIfNotExistsAsync();
                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(PublicAccessType.Blob);
                var blob = container.GetBlobClient(fileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
                return new DataResult<string>
                {
                    Result = blob.Uri.ToString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении файла в хранилище");
                return new DataResult<string>
                {
                    Result = ex.Message
                };
            }
        }
    }
}