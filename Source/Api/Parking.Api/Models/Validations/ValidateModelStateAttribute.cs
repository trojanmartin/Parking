using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Parking.Core.Models;
using Parking.Core.Models.Errors;
using System.Collections.Generic;
using System.Linq;

namespace Parking.Api.Models.Validations
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //var errors = ModelState.Keys.Where(k => ModelState[k].Errors.Count > 0)
                //     .Select(k => new { propertyName = k, errorMessage = ModelState[k].Errors[0].ErrorMessage });


                var errors = context.ModelState.Keys.Where(key => context.ModelState[key].Errors.Count > 0)
                           .Select(key => new { key, context.ModelState[key].Errors })
                           .SelectMany(x => x.Errors, (x, res) => new Error(GlobalErrorCodes.Validation, res.ErrorMessage, new Dictionary<string, object>() { { "invalidField",x.key} }))
                           .ToList();


                var response = new ErrorResponse(errors);

                context.Result = new JsonResult(response)
                {
                    StatusCode = 400
                };
            }
        }
    }
}
