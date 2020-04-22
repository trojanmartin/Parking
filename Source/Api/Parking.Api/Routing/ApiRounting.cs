namespace Parking.Api.Routing
{
   
    public class AccountsRouting
    {
        public const string Login = "/api/accounts/login";
        public const string Register = "/api/accounts/register";
        public const string User = "/api/accounts/user/{username}";
    }

    public class ParkingLotsRouting
    {
        public const string Add = "/api/parkinglots";
        public const string Get = "/api/parkinglots";
    }


    public class ParkingDataRouting
    {
        public const string GetData = "/api/parkingdata/{parkingLotId}";
        public const string Current = "/api/parkingdata/current/{parkingLotId}/{sensorId}";
        public const string Free = "/api/parkingdata/free/{parkingLotId}";
    }
}
