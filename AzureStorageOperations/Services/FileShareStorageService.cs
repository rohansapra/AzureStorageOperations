using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using System.Runtime.InteropServices;

namespace AzureStorageOperations.Services
{
    public class FileShareStorageService : IFileShareStorageService
    {
        private readonly IConfiguration _config;

        public FileShareStorageService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> FileUploadAsync(IFormFile fileDetails, string connectionString, string fileshareName)
        {
            ShareClient shareClient = new ShareClient(connectionString, fileshareName);
            var shareDirectoryClient = shareClient.GetDirectoryClient("");
            var shareFileClient = shareDirectoryClient.GetFileClient(fileDetails.FileName); using (var stream = fileDetails.OpenReadStream())
            {
                shareFileClient.Create(stream.Length);
                await shareFileClient.UploadRangeAsync(new HttpRange(0, fileDetails.Length), stream);
            }
            return true;
        }
        public async Task<byte[]> FileDownloadAsync(string fileShareName, string connectionString, string fileshareName)
        {
            ShareClient shareClient = new ShareClient(connectionString, fileshareName);

            var shareDirectoryClient = shareClient.GetDirectoryClient("");
            var shareFileClient = shareDirectoryClient.GetFileClient(fileShareName);
            var response = await shareFileClient.DownloadAsync();
            using var memoryStream = new MemoryStream();
            await response.Value.Content.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task Delete(string fileName, string connectionString, string fileshareName)
        {
            ShareClient shareClient = new ShareClient(connectionString, fileshareName);

            var directoryClient = shareClient.GetDirectoryClient("");
            var fileClient = directoryClient.GetFileClient(fileName);
            await fileClient.DeleteAsync();
        }
        
    }
}
