using FluentValidation;
using Parking.Api.Models.Request.ParkingLots;

namespace Parking.Api.Models.Validations.ParkingLots
{
    public class ParkingLotRequestValidator : AbstractValidator<ParkingLotRequest>
    {
        public ParkingLotRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull(); ;
            RuleFor(x => x.BoxCount).NotEmpty().NotNull();          
            RuleFor(x => x.Latitude).NotEmpty().NotNull();
            RuleFor(x => x.Longtitude).NotEmpty().NotNull();
        }
    }
}
