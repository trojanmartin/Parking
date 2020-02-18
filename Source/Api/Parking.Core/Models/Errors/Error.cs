namespace Parking.Core.Models
{
    public class Error
    {
        public string Code { get; }

        public string Description { get; }

        public object Data { get; }

        public Error(string code, string description, object data = null)
        {
            Code = code;
            Description = description;
            Data = data;
        }
    }
}
