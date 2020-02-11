using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Parking.Mqtt.Api
{
    public class Program
    {       

        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                              .AddJsonFile("appsettings.json")
                              .Build();

            Log.Logger = new LoggerConfiguration()
                                 .ReadFrom.Configuration(configuration)
                                  .Enrich.FromLogContext()
                                   .CreateLogger();

            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();                
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((hostingContext, config) =>
               {
                   //if (hostingContext.HostingEnvironment.IsDevelopment())
                   config.AddJsonFile("appsettings.Development.json", optional: true);

                   //else        

                   config.AddEnvironmentVariables();

               })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
