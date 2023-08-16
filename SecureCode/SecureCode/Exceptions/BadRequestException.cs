using System.Net;

namespace SecureCode.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message, null, HttpStatusCode.BadRequest)
        {
        }
    }
}
