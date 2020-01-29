namespace Parking.Core.Interfaces.Base
{
    public abstract class BaseUseCaseResponseMessage
    {
        public bool Success { get; }
        public string Message { get; }

        protected BaseUseCaseResponseMessage(bool success = false, string message = null)
        {
            Success = success;
            Message = message;
        }
    }
}
