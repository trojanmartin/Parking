using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.Errors
{
    public static class GlobalErrors
    {
        public static Error UnexpectedError => new Error(GlobalErrorCodes.InternalServer, "Unexpected error occurs while proccesing your request.");
    }

    public static class GlobalErrorCodes
    {
        public static string InternalServer => "InternalServerError";

        public static string NotFound => "NotFound";
    }

}