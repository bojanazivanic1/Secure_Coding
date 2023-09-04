using System.Net;

namespace InsecureCode.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message, null, HttpStatusCode.BadRequest)
        {
        }
    }
}
