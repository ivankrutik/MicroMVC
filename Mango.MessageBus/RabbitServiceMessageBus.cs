using Mango.MessageBus.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Mango.MessageBus
{
    public class RabbitServiceMessageBus : IMessageBus
    {
        //to config
        private const string hostString = "localhost";
        private const string queueName = "MyQueue";
        private ConnectionFactory factory;
        private const int rabbitMqPort = 15672;

        public RabbitServiceMessageBus()
        {
            factory = new ConnectionFactory()
            {
                HostName = hostString,
                Port = rabbitMqPort
            };
        }

        public void PublishMessage(BaseMessage message, string topic)
        {
            var json = JsonConvert.SerializeObject(message);
            var connection = factory.CreateConnection();
            using (connection)
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                   durable: false,
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null);

                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "",
                                   routingKey: queueName,
                                   basicProperties: null,
                                   body: body);
                }
            }
        }
    }
}
