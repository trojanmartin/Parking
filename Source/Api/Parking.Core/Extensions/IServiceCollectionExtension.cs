using Microsoft.Extensions.DependencyInjection;
using Parking.Core.Handlers;
using Parking.Core.Interfaces.Handlers;

namespace Parking.Core.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection services) =>

            services.AddTransient<IAccountsHandler, AccountsHandler>()
            .AddTransient<IParkingDataHandler, ParkingDataHandler>()
            .AddTransient<IParkingLotHandler, ParkingLotHandler>()
                                 
            
            ;
        
    }
}
