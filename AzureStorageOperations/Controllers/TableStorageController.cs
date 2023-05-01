using AzureStorageOperations.Models;
using AzureStorageOperations.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageOperations.Controllers
{
    [Route("api/[controller]")]
    public class TableStorageController : Controller
    {
        private readonly string _connectionString;
        private readonly ITableStorageService _storageService;

        public TableStorageController(ITableStorageService storageService, IConfiguration iConfig)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _connectionString = iConfig.GetValue<string>("MyConfig:StorageConnection");
        }

        [HttpGet]
        [Route("GetTableData")]
        public async Task<IActionResult> GetAsync([FromQuery] string category, string id)
        {
            return Ok(await _storageService.GetEntityAsync(category, id, _connectionString));
        }

        [HttpPost]
        [Route("InsertTableData")]
        public async Task<IActionResult> PostAsync([FromForm] GroceryItemEntity entity)
        {
            entity.PartitionKey = entity.Category;
            string Id = Guid.NewGuid().ToString();
            entity.Id = Id;
            entity.RowKey = Id;
            var createdEntity = await _storageService.UpsertEntityAsync(entity, _connectionString);
            return Ok(true);
        }

        [HttpPut]
        [Route("UpdateTableData")]
        public async Task<IActionResult> PutAsync([FromForm] GroceryItemEntity entity)
        {
            entity.PartitionKey = entity.Category;
            entity.RowKey = entity.Id;
            await _storageService.UpsertEntityAsync(entity, _connectionString);
            return Ok(true);
        }

        [HttpDelete]
        [Route("DeleteTableData")]
        public async Task<IActionResult> DeleteAsync([FromQuery] string category, string id)
        {
            await _storageService.DeleteEntityAsync(category, id, _connectionString);
            return Ok(true);
        }
    }
}
