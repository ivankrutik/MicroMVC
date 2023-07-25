using Mango.Services.PaymentAPI.Messages;
using Mango.Services.PaymentAPI.Messages.Pay;
using MessageBus.Producer;
using Newtonsoft.Json;
using PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Mango.Services.PaymentAPI.Messaging
{
    public class ServiseBusConsumer : IServiseBusConsumer
    {
        private readonly IProcessPayment _processPayment;
        private readonly string _queuePayName = "payQueue";
        private readonly string _queueUpdateOrderName = "updateOrder";
        private readonly IMessageProducer _messageProducer;
        private EventingBasicConsumer _consumer;
        private IModel _channel;

        public ServiseBusConsumer(IProcessPayment processPayment, IMessageProducer messageProducer)
        {
            _processPayment = processPayment;
            _messageProducer = messageProducer;
            StartListen();
        }

        private void StartListen()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            factory.UserName = "user";
            factory.Password = "password";

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: _queuePayName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += OnCheckOutMessageReceived;

            _channel.BasicConsume(queue: _queuePayName, autoAck: true, consumer: _consumer);
        }

        private async void OnCheckOutMessageReceived(object model, BasicDeliverEventArgs eventArgs)
        {

            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            if (message == null)
            {
                throw new Exception("Сообщенеие из очереди не десериализовано");
            }
            //здесь надо достать информацию из очереди сообщений
            PaymentRequestMessage paymentRequest = JsonConvert.DeserializeObject<PaymentRequestMessage>(message);
            var result = _processPayment.PaymentProcessor();
            UpdatePaymentResultMessage updatePaymentResultMessage = new UpdatePaymentResultMessage 
            {
                OrderId = paymentRequest.OrderId,
                Status = result
            };            

            try
            {
                _messageProducer.SendMessage(updatePaymentResultMessage, _queueUpdateOrderName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
