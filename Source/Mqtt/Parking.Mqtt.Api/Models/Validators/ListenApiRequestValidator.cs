using FluentValidation;
using Parking.Mqtt.Api.Models.Requests;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Models.Validators
{
    public class ListenApiRequestValidator : AbstractValidator<ListenApiRequest>
    {
        public ListenApiRequestValidator()
        {
            RuleFor(x => x.Topics).NotNull();

            RuleForEach(x => x.Topics).ChildRules(topics =>
            {
                topics.RuleFor(en => en.QoS).IsInEnum().NotEmpty().WithMessage("Undefined QoS");
                topics.RuleFor(top => top.TopicName).NotEmpty().WithMessage("Topic cannot be empty");
            });
        }
    }
}
