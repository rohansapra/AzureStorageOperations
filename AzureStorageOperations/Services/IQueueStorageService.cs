using Azure.Storage.Queues.Models;

namespace AzureStorageOperations.Services
{
    public interface IQueueStorageService
    {
        bool CreateQueue(string connectionString, string queueName);
        bool SendMessageToQueue(string connectionString, string queueName, string message);
        PeekedMessage PeekMessage(string connectionString, string queueName);
        bool UpdateMessage(string connectionString, string queueName, string newMessage);
        bool DeleteQueue(string connectionString, string queueName);
    }
}
