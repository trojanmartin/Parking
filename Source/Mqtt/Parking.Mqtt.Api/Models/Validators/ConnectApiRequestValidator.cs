using FluentValidation;
using Parking.Mqtt.Api.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Models.Validators
{
    public class ConnectApiRequestValidator : AbstractValidator<ConnectApiRequest>
    {
        public ConnectApiRequestValidator()
        {
            RuleFor(x => x.TcpServer).NotEmpty().NotNull();
            RuleFor(x => x.Port).NotEmpty().NotNull();
            RuleFor(x => x.UseTls).NotEmpty().NotNull();           
        }
    }
}
