using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Services
{
    public class MyNServiceBus
    {
        public static void SendMessage(string machineName, string queueName, string userName, string password, string messageBody, Dictionary<string, object> headers)
        {
            using (var connection = OpenConnection(machineName, userName, password))
            using (var channel = connection.CreateModel())
            {
                var properties = channel.CreateBasicProperties();
                properties.MessageId = Guid.NewGuid().ToString();
                properties.Headers = headers;
                var body = Encoding.UTF8.GetBytes(messageBody);
                channel.BasicPublish(string.Empty, queueName, false, properties, body);
            }
        }

        static IConnection OpenConnection(string machine, string userName, string password)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = machine,
                Port = AmqpTcpEndpoint.UseDefaultPort,
                UserName = userName,
                Password = password,
            };

            return connectionFactory.CreateConnection();
        }
    }
}
