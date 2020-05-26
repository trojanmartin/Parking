using System.Collections.Generic;
using System.Linq;

namespace Parking.Mqtt.Core.Models.Errors
{
    public class ErrorResponse
    {

        public ErrorResponse(IEnumerable<Error> errors)
        {
            Errors = errors;
        }

        public int? Count => Errors?.Count();

        public IEnumerable<Error> Errors { get; }

    }
}
