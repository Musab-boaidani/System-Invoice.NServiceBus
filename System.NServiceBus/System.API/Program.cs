using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using NServiceBus;
using System.Domain.NServiceBusMessages;
using System.Threading.Tasks;

namespace System.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "System";
            CreateHostBuilder(args).Build().Run();
        }
     
    public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).UseNServiceBus(context =>
            {
                var endpointConfiguration = new EndpointConfiguration("System");
                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                transport.UseConventionalRoutingTopology();
                transport.ConnectionString("host=localhost;username=admin;password=123456");
                var routing = transport.Routing();
                routing.RouteToEndpoint(typeof(OrderId).Assembly, "Invoice");
                endpointConfiguration.EnableInstallers();
               
                return endpointConfiguration;


            }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
