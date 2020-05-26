using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Parking.Api.AddedSchemas;
using Parking.Api.Extensions;
using Parking.Api.Models.Validations;
using Parking.Api.Swasbuckle;
using Parking.Core.Extensions;
using Parking.Database;
using Parking.Database.Entities.Identity;
using Parking.Infrastructure.Auth;
using Parking.Infrastructure.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Parking.Api
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
            //adding controllers and setting for fluent validation
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            ValidatorOptions.LanguageManager.Enabled = false;

            services.Configure<ApiBehaviorOptions>(opttions =>
            {
                opttions.SuppressModelStateInvalidFilter = true;
            });

            //// Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Parking API", Version = "v1" });
                c.AddFluentValidationRules();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.DocumentFilter<CustomModelDocumentFilter<ValidationErrorResponse>>();

            });

            //adding db context
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"),
                                                        x => x.MigrationsAssembly("Parking.Database")));


            //configure options for jwt 
            services.Configure<JwtTokenOptions>(x =>
            {
                x.Audience = Configuration["JwtTokenOptions:Audience"];
                x.Issuer = Configuration["JwtTokenOptions:Issuer"];
                x.ValidTo = Convert.ToInt32(Configuration["JwtTokenOptions:ValidTo"]);
                x.SecretKey = Configuration["JwtTokenOptions:SecretKey"];
            });


            //adding all requested module for application
            services.AddApiModule()
                    .AddCoreModule()
                    .AddInfrastructureModule();

            // AddIdentity adds cookie based authentication
            // Adds scoped classes for things like UserManager, SignInManager, PasswordHashers etc..
            // NOTE: Automatically adds the validated user from a cookie to the HttpContext.User
            // https://github.com/aspnet/Identity/blob/85f8a49aef68bf9763cd9854ce1dd4a26a7c5d3c/src/Identity/IdentityServiceCollectionExtensions.cs
            services.AddIdentity<AppUser, IdentityRole>(options =>
           {
               options.Password.RequireDigit = false;
               options.Password.RequiredLength = 5;
               options.Password.RequireLowercase = true;
               options.Password.RequireUppercase = false;
               options.Password.RequireNonAlphanumeric = false;
           })
                // Adds UserStore and RoleStore from this context
                // That are consumed by the UserManager and RoleManager               
                .AddEntityFrameworkStores<ApplicationDbContext>()
                // Adds a provider that generates unique keys and hashes for things like
                // forgot password links, phone number verification codes etc...
                .AddDefaultTokenProviders();


            //configure JWt token Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["JwtTokenOptions:Issuer"],
                    ValidAudience = Configuration["JwtTokenOptions:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["JwtTokenOptions:SecretKey"])),
                };
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Parking API V1");
                c.RoutePrefix = string.Empty;

            });

            // Setup Identity
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Make sure we have database
          // serviceProvider.GetService<ApplicationDbContext>().Database.EnsureCreated();
        }
    }
}
