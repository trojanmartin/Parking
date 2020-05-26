using Parking.Api.Presenters.Base;
using Parking.Api.Serialization;
using Parking.Core.Interfaces.Base;
using Parking.Core.Models.ParkingLots.Responses;
using System.Net;

namespace Parking.Api.Presenters.ParkingLots
{
    public class GetParkingLotsPresenter : BasePresenter, IOutputPort<GetParkingLotsResponseDTO>
    {
        public void CreateResponse(GetParkingLotsResponseDTO response)
        {
            Result.Content = response.Success ? Serializer.SerializeObjectToJson(response.ParkingLots) : Serializer.SerializeObjectToJson(response.ErrorResponse);
            Result.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
        }
    }
}
