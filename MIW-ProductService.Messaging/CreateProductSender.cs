

using System.Text;
using MIW_ProductService.Dal.Models;
using RabbitMQ.Client;

namespace MIW_ProductService.Messaging
{
    public static class CreateProductSender
    {
        public static void Send(Product product)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.QueueDeclare(queue: "CreateProduct",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            string message = CreateMessage(product).GetJsonStr();
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                routingKey: "CreateProduct",
                basicProperties: null,
                body: body);
        }

        private static ProductMessage CreateMessage(Product product)
        {
            return new()
            {
                Product = product,
                MessageType = MessageType.Create
            };
        }
    }
}

