using Parking.Core.Interfaces.Base;

namespace Parking.Api.AddedSchemas
{
    /// <summary>
    /// Error which is returned when some validation errors occurs
    /// </summary>
    public class ValidationErrorResponse
    {
        public bool Success { get; }
        public string Message { get; }

        public Data Data { get; }
    }

    public class Data
    {
        public string InvalidField { get; }
    }
}
