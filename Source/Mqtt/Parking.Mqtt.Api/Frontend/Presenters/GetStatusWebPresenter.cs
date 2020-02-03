using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.UseCaseResponses;

namespace Parking.Mqtt.Api.Frontend.Presenters
{
    public class GetStatusWebPresenter : IOutputPort<GetStatusResponse>
    {
        public GetStatusResponse Response { get; set; }

        public void CreateResponse(GetStatusResponse response)
        {
            Response = response;
        }
    }
}
