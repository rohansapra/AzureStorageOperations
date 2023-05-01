using Azure.Data.Tables;
using AzureStorageOperations.Models;

namespace AzureStorageOperations.Services
{
    public class TableStorageService : ITableStorageService
    {
        private const string TableName = "Item";
        private readonly IConfiguration _configuration;
        public TableStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<TableClient> GetTableClient(string connectionString)
        {
            var serviceClient = new TableServiceClient(connectionString);
            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }

        public async Task<GroceryItemEntity> GetEntityAsync(string category, string id, string connectionString)
        {
            var tableClient = await GetTableClient(connectionString);
            return await tableClient.GetEntityAsync<GroceryItemEntity>(category, id);
        }
        public async Task<GroceryItemEntity> UpsertEntityAsync(GroceryItemEntity entity, string connectionString)
        {
            var tableClient = await GetTableClient(connectionString);
            await tableClient.UpsertEntityAsync(entity);
            return entity;
        }
        public async Task DeleteEntityAsync(string category, string id, string connectionString)
        {
            var tableClient = await GetTableClient(connectionString);
            await tableClient.DeleteEntityAsync(category, id);
        }
    }
}
