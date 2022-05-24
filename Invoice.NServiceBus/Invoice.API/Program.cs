using Invoice.Domain.NServiceBusMessages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NServiceBus;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Invoice";
            CreateHostBuilder(args).Build().Run();
        }

       
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
             return Host.CreateDefaultBuilder(args).UseNServiceBus(context =>
             {
                 var endpointConfiguration = new EndpointConfiguration("Invoice");
                 var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

                 transport.UseConventionalRoutingTopology();


                 transport.ConnectionString("host=localhost;username=admin;password = 123456");

                 var routing = transport.Routing();
                 routing.RouteToEndpoint(typeof(InvoiceId), "System");
                 endpointConfiguration.SendFailedMessagesTo("error");
                 endpointConfiguration.AuditProcessedMessagesTo("audit");
                 endpointConfiguration.EnableInstallers();
                 //endpointConfiguration.SendFailedMessagesTo("error");
                 //endpointConfiguration.AuditProcessedMessagesTo("audit
                 //    delayed =>
                 //    {");
                 //endpointConfiguration.SendHeartbeatTo("Particular.ServiceControl");
                 //var recoverablility = endpointConfiguration.Recoverability();
                 //recoverablility.Delayed(
                 //        delayed.TimeIncrease(TimeSpan.FromSeconds(2));
                 //    }
                 //);
                 //var metrics = endpointConfiguration.EnableMetrics();
                 //metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));

                 return endpointConfiguration;

             })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseDefaultServiceProvider(options =>
    options.ValidateScopes = false);
        }
    }
}
