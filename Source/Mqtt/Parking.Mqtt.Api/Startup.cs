using AutoMapper;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Parking.Database;
using Parking.Mqtt.Api.Extensions;
using Parking.Mqtt.Core.Extensions;
using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Infrastructure.Extensions;
using Serilog;
using System;

namespace Parking.Mqtt.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());       


            //adding all services to dependency injection container
            services.AddApiModule(Configuration)                  
                    .AddCoreModule()
                    .AddInfrastructureModule();


            var connectionString = Configuration.GetConnectionString("Default");

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString, 
                                        x => x.MigrationsAssembly("Parking.Database")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddHangfire(configuration => configuration
                                     .UseSqlServerStorage(connectionString, new SqlServerStorageOptions()
                                     {

                                     }));

            services.AddHangfireServer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider,  IHostApplicationLifetime hostApplicationLifetime)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                      
            });


            RecurringJob.AddOrUpdate(() => serviceProvider.GetService<IMQTTDataHandler>().NormalizeFromCacheAndSaveToDBAsync(), "*/1 * * * *");


            //serviceProvider.GetService<ApplicationDbContext>().Database.EnsureCreated();


            //serviceProvider.GetService<ApplicationDbContext>().MqttServerConfigurations.Add(server);
            //serviceProvider.GetService<ApplicationDbContext>().SaveChanges();
        }
    }
}
