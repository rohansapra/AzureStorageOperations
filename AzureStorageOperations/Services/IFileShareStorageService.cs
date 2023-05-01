namespace AzureStorageOperations.Services
{
    public interface IFileShareStorageService
    {
        Task<bool> FileUploadAsync(IFormFile FileDetail, string connectionString, string fileshareName);
        Task<byte[]> FileDownloadAsync(string fileShareName, string connectionString, string fileshareName);

        Task Delete(string fileName, string connectionString, string fileshareName);
    }
}
