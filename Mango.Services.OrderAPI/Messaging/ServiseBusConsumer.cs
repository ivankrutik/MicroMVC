using Mango.Services.OrderAPI.Messages;
using Mango.Services.OrderAPI.Messages.Pay;
using Mango.Services.OrderAPI.Models;
using Mango.Services.OrderAPI.Repository;
using MessageBus.Producer;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Mango.Services.OrderAPI.Messaging
{
    public class ServiseBusConsumer : IServiseBusConsumer
    {
        private readonly OrderRepository _orderRepository;
        private readonly string _queueName = "testQueue";
        private readonly string _queuePayName = "payQueue";
        private readonly IMessageProducer _messageProducer;
        private EventingBasicConsumer _consumer;
        private IModel _channel;

        public ServiseBusConsumer(OrderRepository orderRepository, IMessageProducer messageProducer)
        {
            _orderRepository = orderRepository;
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

            _channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += OnCheckOutMessageReceived;

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: _consumer);
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
            CheckOutHeaderDto checkOutHeaderDto = JsonConvert.DeserializeObject<CheckOutHeaderDto>(message);

            OrderHeader orderHeader = new OrderHeader()
            {
                UserId = checkOutHeaderDto.UserId,
                FirstName = checkOutHeaderDto.FirstName,
                LastName = checkOutHeaderDto.LastName,
                OrderDetails = new List<OrderDetail>(),
                CardNumber = checkOutHeaderDto.CardNumber,
                CouponCode = checkOutHeaderDto.CouponCode,
                CVV = checkOutHeaderDto.CVV,
                DiscountTotal = checkOutHeaderDto.DiscountTotal,
                EMail = checkOutHeaderDto.EMail,
                ExpiryMonthYear = checkOutHeaderDto.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                OrderTotal = checkOutHeaderDto.OrderTotal,
                PaymentStatus = false,
                Phone = checkOutHeaderDto.Phone,
                PickupDateTime = checkOutHeaderDto.PickupDateTime
            };

            foreach (var item in checkOutHeaderDto.CartDetails)
            {
                var orderDetails = new OrderDetail()
                {
                    ProductId = (long)item.ProductId,
                    ProductName = item.Product.Name,
                    Price = item.Product.Price,
                    Count = (int)item.Count
                };
                orderHeader.CartTotalItems += orderDetails.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await _orderRepository.AddOrder(orderHeader);

            PaymentRequestMessage paymentRequestMessage = new PaymentRequestMessage
            {
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                OrderId = orderHeader.OrderHeaderId,
                OrderTotal = orderHeader.OrderTotal
            };

            try
            {
                _messageProducer.SendMessage(paymentRequestMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
