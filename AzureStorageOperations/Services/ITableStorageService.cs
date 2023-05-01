using AzureStorageOperations.Models;

namespace AzureStorageOperations.Services
{
    public interface ITableStorageService
    {
        Task<GroceryItemEntity> GetEntityAsync(string category, string id, string connectionString);
        Task<GroceryItemEntity> UpsertEntityAsync(GroceryItemEntity entity, string connectionString);
        Task DeleteEntityAsync(string category, string id, string connectionString);
    }
}
