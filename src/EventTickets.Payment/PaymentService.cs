using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventTickets.Payment
{
    public class PaymentService : BackgroundService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;

        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("send-payment", false, false, false, null);
            _consumer = new EventingBasicConsumer(_channel);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Received += async (model, content) =>
            {
                var body = content.Body.ToArray();
                var basketId = Encoding.UTF8.GetString(body);
                await PayAsync(Guid.Parse(basketId), cancellationToken);
                NotifyPaymentDone();
            };

            _channel.BasicConsume("send-payment", true, _consumer);
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }

        private void NotifyPaymentDone()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("receive-payment", false, false, false, null);
                    var message = "Payment Successful!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", "receive-payment", null, body);
                }
            }
        }
        private async Task PayAsync(Guid basketId, CancellationToken cancellationToken)
        {
            await Task.Delay(60000, cancellationToken);
            Console.WriteLine($"Paying basket {basketId}");
        }
    }
}
