using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.Gateways.Services.Mqtt
{
    public abstract class BaseGatewayResponse
    {
        public BaseGatewayResponse(bool succes= false, IEnumerable<Error> errors = null)
        {
            Succes = succes;
            Errors = errors;
        }

        public bool Succes { get; }
        public IEnumerable<Error> Errors { get; }
    }
}