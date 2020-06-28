using ReentregaPB.Dominio.Model.Interfaces.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Text;
using Azure.Storage.Queues;

namespace ReentregaPB.Infraestrutura.Services.Queue
{
    public class QueueMessage : IQueueMessage
    {
        private readonly QueueServiceClient _queueServiceClient;
        private const string _queueName = "queue-image-insert";

        public QueueMessage(string storageAccount)
        {
            _queueServiceClient = new QueueServiceClient(storageAccount);
        }

        public async System.Threading.Tasks.Task SendAsync(string messageText, string fila)
        {
            var queueClient = _queueServiceClient.GetQueueClient(fila);

            await queueClient.CreateIfNotExistsAsync();

            await queueClient.SendMessageAsync(messageText);
        }

    }
}
