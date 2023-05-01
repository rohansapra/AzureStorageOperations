using AzureStorageOperations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageOperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobStorageController : ControllerBase
    {
        private readonly IBlobStorageService _storageService;
        private readonly string _connectionString;
        private readonly string _container;

        public BlobStorageController(IBlobStorageService storageService, IConfiguration iConfig)
        {
            _storageService = storageService;
            _connectionString = iConfig.GetValue<string>("MyConfig:StorageConnection");
            _container = iConfig.GetValue<string>("MyConfig:ContainerName");
        }

        [HttpGet("ListFiles")]
        public async Task<List<string>> ListFiles()
        {
            return await _storageService.GetAllDocuments(_connectionString, _container);
        }

        [Route("InsertFile")]
        [HttpPost]
        public async Task<bool> InsertFile(IFormFile asset)
        {
            if (asset != null)
            {
                Stream stream = asset.OpenReadStream();
                await _storageService.UploadDocument(_connectionString, _container, asset.FileName, stream);
                return true;
            }

            return false;
        }

        [HttpGet("DownloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var content = await _storageService.GetDocument(_connectionString, _container, fileName);
            return File(content, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [Route("DeleteFile/{fileName}")]
        [HttpDelete]
        public async Task<bool> DeleteFile(string fileName)
        {
            return await _storageService.DeleteDocument(_connectionString, _container, fileName);
        }
    }
}
