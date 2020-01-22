using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Parking.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var hash = Convert.FromBase64String("AQAAAAEAACcQAAAAEC+6mW3Rp8RvJsXGCfNtWBhZKuI5K8fCdRKL1f7wuzPqMpmxlYE84PYcMCbXZnONsg==");


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
                .ConfigureAppConfiguration( (hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                    
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
