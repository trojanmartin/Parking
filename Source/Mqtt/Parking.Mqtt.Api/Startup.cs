using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Parking.Mqtt.Api.Extensions;
using Parking.Mqtt.Core.Extensions;
using Parking.Mqtt.Infrastructure.Extensions;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Parking.Mqtt.Api.Data;

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

            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.RootDirectory = "/Frontend/Pages";
            });


            //adding all services to dependency injection container
            services.AddApiModule()
                    .AddAdministrationModule()
                    .AddCoreModule()
                    .AddInfrastructureModule();

            services.AddDbContext<ParkingMqttApiContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ParkingMqttApiContext")));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
                endpoints.MapRazorPages();               
            });
        }
    }
}
