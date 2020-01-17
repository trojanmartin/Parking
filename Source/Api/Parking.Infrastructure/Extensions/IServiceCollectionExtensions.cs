﻿using Microsoft.Extensions.DependencyInjection;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Interfaces.Gateways.Services;
using Parking.Infrastructure.Auth;
using Parking.Infrastructure.Data.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {
            return services
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IJwtTokenFactory, JwtTokenFactory>()     
                ;
        }
    }
}
