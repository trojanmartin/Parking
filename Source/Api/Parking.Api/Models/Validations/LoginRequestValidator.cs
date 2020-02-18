using FluentValidation;
using Parking.Api.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Api.Models.Validations
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty().NotNull();
        }
    }
}
