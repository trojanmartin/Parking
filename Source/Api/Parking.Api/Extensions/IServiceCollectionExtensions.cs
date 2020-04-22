using Microsoft.Extensions.DependencyInjection;
using Parking.Api.Presenters;
using Parking.Api.Presenters.ParkingData;
using Parking.Api.Presenters.ParkingLots;

namespace Parking.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            return services.AddTransient<LoginPresenter>()
                           .AddTransient<RegisterPresenter>()
                           .AddTransient<GetUserPresenter>()
                           .AddTransient<StandardResponsePresenter>()
                           .AddTransient<GetParkingLotsPresenter>()
                           .AddTransient<ParkingDataResponsePresenter>()


                           ;
        }
    }
}
