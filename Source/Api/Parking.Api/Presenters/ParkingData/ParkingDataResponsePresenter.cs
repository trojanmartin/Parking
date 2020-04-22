using Parking.Api.Presenters.Base;
using Parking.Api.Serialization;
using Parking.Core.Interfaces.Base;
using Parking.Core.Models.ParkData.Responses;
using System.Net;

namespace Parking.Api.Presenters.ParkingData
{
    public class ParkingDataResponsePresenter :BasePresenter, IOutputPort<GetParkingDataResponseDTO>
    {
        public void CreateResponse(GetParkingDataResponseDTO response)
        {
            Result.Content = response.Success ? Serializer.SerializeObjectToJson(response.ParkingSpots) : Serializer.SerializeObjectToJson(response.ErrorResponse);
            Result.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
        }
    }
}
