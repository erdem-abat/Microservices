using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Microservices.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private string connectionString = "access policies con string 105. video";
        public async Task PublishMessage(object message, string topic_queue_name)
        {   
            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(topic_queue_name);

            var jsonMessage = JsonConvert.SerializeObject(message);

            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();
        }
    }
}
