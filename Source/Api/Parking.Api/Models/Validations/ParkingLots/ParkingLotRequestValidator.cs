using FluentValidation;
using Parking.Api.Models.Request.ParkingLots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Api.Models.Validations.ParkingLots
{
    public class ParkingLotRequestValidator : AbstractValidator<ParkingLotRequest>
    {
        public ParkingLotRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull(); ;
            RuleFor(x => x.BoxCount).NotEmpty().NotNull();          
            RuleFor(x => x.Latutide).NotEmpty().NotNull();
            RuleFor(x => x.Longitude).NotEmpty().NotNull();
        }
    }
}
