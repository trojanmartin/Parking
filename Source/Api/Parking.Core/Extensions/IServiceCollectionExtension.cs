using Microsoft.Extensions.DependencyInjection;
using Parking.Core.Interfaces.Gateways.Services;
using Parking.Core.Interfaces.UseCases;
using Parking.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection services) =>

            services.AddTransient<ILoginUseCase, LoginUseCase>()
                    .AddTransient<IRegisterUseCase, RegisterUserUseCase>()                    
            
            ;
        
    }
}
