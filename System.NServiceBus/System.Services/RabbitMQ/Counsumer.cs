﻿
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Services.RabbitMQ.Produser;
using System.Text;

namespace System.Services.RabbitMQ
{

    public class Counsumer
    {
       
       


       

        public static void StartCounsumer()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName ="admin",
                Password ="123456"
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "invoices", exclusive: false, durable: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message received: {message}");


            };

            channel.BasicConsume(queue: "invoices", autoAck: true, consumer: consumer);

        }
    }
}
