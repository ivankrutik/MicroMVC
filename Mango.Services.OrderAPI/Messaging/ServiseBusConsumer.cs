using Mango.Services.OrderAPI.Messages;
using Mango.Services.OrderAPI.Models;
using Mango.Services.OrderAPI.Repository;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;
using System.Timers;

namespace Mango.Services.OrderAPI.Messaging
{
    public class ServiseBusConsumer : IServiseBusConsumer
    {
        private readonly OrderRepository _orderRepository;
        private readonly string _queueName = "testQueue";
        private EventingBasicConsumer _consumer;
        private IModel _channel;



        public ServiseBusConsumer(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Start()
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
            _consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: _consumer);
        }

        private async void OnCheckOutMessageReceived(object model, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            //здесь надо достать информацию из очереди сообщений
            CheckOutHeaderDto checkOutHeaderDto = new CheckOutHeaderDto();

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
        }
    }
}
