using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace AzureStorageOperations.Services
{
    public class QueueStorageService : IQueueStorageService
    {

        public bool CreateQueue(string connectionString, string queueName)
        {
            try
            {
                QueueClient queueClient = new QueueClient
                (connectionString, queueName);
                queueClient.CreateIfNotExists();
                if (queueClient.Exists())
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool SendMessageToQueue(string connectionString, string queueName, string message)
        {
            try
            {
                QueueClient queueClient = new QueueClient
                (connectionString, queueName);
                queueClient.CreateIfNotExists();
                if (queueClient.Exists())
                {
                    queueClient.SendMessage(message);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public PeekedMessage PeekMessage(string connectionString, string queueName)
        {
            PeekedMessage? peekedMessage = null;
            try
            {
                QueueClient queueClient = new QueueClient
                    (connectionString, queueName);
                if (queueClient.Exists())
                {
                    peekedMessage =
                    queueClient.PeekMessage();
                    var messageBody = peekedMessage.Body;
                    //Write your code here to work with the message body
                }
                return peekedMessage;
            }
            catch
            {
                return peekedMessage;
            }
        }

        public bool UpdateMessage(string connectionString, string queueName, string newMessage)
        {
            try
            {
                QueueClient queueClient = new QueueClient
                (connectionString, queueName);
                if (queueClient.Exists())
                {
                    QueueMessage[] message = queueClient.ReceiveMessages();
                    queueClient.UpdateMessage(message[0].MessageId, message[0].PopReceipt, newMessage);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteQueue(string connectionString, string queueName)
        {
            try
            {
                QueueClient queueClient = new QueueClient
                (connectionString, queueName);
                if (queueClient.Exists())
                {
                    queueClient.Delete();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
