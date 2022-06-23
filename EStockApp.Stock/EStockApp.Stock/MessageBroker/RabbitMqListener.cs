using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace EStockApp.Stock.MessageBroker
{
    public class RabbitMqListener:IRabbitMqListener
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel channel;
        private string exchangeName = "stockQueue";
        public RabbitMqListener(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            createChannel();
        }
        public void createChannel()
        {
            if (_connection == null || _connection.IsOpen == false)
            {
                _connection = _connectionFactory.CreateConnection();
            }
            if (channel == null||channel.IsOpen==false)
            {
                channel = _connection.CreateModel();
                channel.QueueDeclare(queue: exchangeName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            }
        }
        public void Receive()
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            var consumerTag=channel.BasicConsume(queue: exchangeName,
                                    autoAck: true,
                                    consumer: consumer
                                   );
            channel.BasicCancel(consumerTag);
        }
        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "",
                                routingKey: exchangeName,
                                basicProperties: null,
                                body: body);
        }
        private void Consumer_Received(object sender,BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
        }
    }
}
