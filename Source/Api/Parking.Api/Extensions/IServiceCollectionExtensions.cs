using Microsoft.Extensions.DependencyInjection;
using Parking.Api.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            return services.AddTransient<LoginPresenter>()
                           .AddTransient<RegisterPresenter>()


                           ;
        }
    }
}
