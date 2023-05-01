using AzureStorageOperations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageOperations.Controllers
{
    public class FileShareStorageController : Controller
    {
        private readonly IFileShareStorageService fileShareStorageService;
        private readonly string _connectionString;
        private readonly string _fileShareString;

        public FileShareStorageController(IFileShareStorageService _fileShareStorageService, IConfiguration iConfig)
        {
            this.fileShareStorageService = _fileShareStorageService;
            _connectionString = iConfig.GetValue<string>("MyConfig:StorageConnection");
            _fileShareString = iConfig.GetValue<string>("FileShareDetails:FileShareName");
        }

        /// <summary>
        /// upload file
        /// </summary>
        /// <param name="fileDetail"></param>
        /// <returns></returns>
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFile(IFormFile fileDetail)
        {
            if (fileDetail != null)
            {
                await fileShareStorageService.FileUploadAsync(fileDetail, _connectionString, _fileShareString);
            }
            return Ok();
        }

        /// <summary>
        /// download file
        /// </summary>
        /// <param name="fileDetail"></param>
        /// <returns></returns>
        [HttpPost("Download")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var file = await fileShareStorageService.FileDownloadAsync(fileName, _connectionString, _fileShareString);
            if (file != null)
            {
                return File(file, "application/octet-stream", fileName);
            }
            return NotFound();
        }

        /// <summary>
        /// download file
        /// </summary>
        /// <param name="fileDetail"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            if (fileName != null)
            {
                await fileShareStorageService.Delete(fileName, _connectionString, _fileShareString);
            }
            return Ok();
        }
    }
}
