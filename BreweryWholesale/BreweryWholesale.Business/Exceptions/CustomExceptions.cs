using System.Net;

namespace BreweryWholesale.Infrastructure.Exceptions
{
    public class CustomExceptions : Exception
    {
        public int StatusCode { get; set; }
        public CustomExceptions(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
