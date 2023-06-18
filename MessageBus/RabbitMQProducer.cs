using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MessageBus
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly string _queueName = "testQueue";

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            factory.UserName = "user";
            factory.Password = "password";

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(_queueName);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: _queueName, body: body);


        }
    }
}