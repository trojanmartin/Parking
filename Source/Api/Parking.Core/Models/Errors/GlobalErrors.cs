namespace Parking.Core.Models.Errors
{
    public static class GlobalErrors
    {
        public static Error UnexpectedError => new Error("Internal server error", "Unexpected error occurs while proccesing your request.");
    }
}
