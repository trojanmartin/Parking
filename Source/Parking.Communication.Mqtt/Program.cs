using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parking.Models.Mqtt.Options;
using Parking.Communication.Mqtt.Handlers;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Parking.Communication.Mqtt
{
    public class Program 
    {

        private static IConfiguration Configuration { get; set; }

        static async Task Main(string[] args)
        {
            //get configuration from json
            Configuration = BuildConfiguration();

            //configure DI
            var services = ConfigureServices();

            //build DI service provider
            var provider = services.BuildServiceProvider();

            //run application
            await provider.GetService<Application>().Execute();
        
        }

        private static IServiceCollection ConfigureServices()
        { 
            return new ServiceCollection()
                .AddTransient<Application>()
                .AddScoped<IMqttProvider, MqttProvider>()
                .AddScoped<IMessageHandler, MessageHandler>()
                .AddOptions()
                .Configure<MqttOptions>(Configuration.GetSection("MqttOptions"));
        }

        private static IConfiguration BuildConfiguration()
        {
            string assemblypath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);           

            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())                              
                              .AddJsonFile(Path.Combine(assemblypath, "appsettings.json"), optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
