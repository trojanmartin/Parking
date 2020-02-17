namespace Parking.Core.Models.Errors
{
    public static class GlobalErrors
    {
        public static Error UnexpectedError => new Error(GlobalErrorCodes.InternalServer, "Unexpected error occurs while proccesing your request.");
    }

    public static class GlobalErrorCodes
    {
        public static string InternalServer => "internal_server_error";

        public static string NotFound => "not_found";

        public static string InvalidCredentials => "invalid_credentials";
        public static string Validation => "validation_error";


    }
}
