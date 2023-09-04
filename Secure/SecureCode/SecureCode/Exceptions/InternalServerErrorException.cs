using System.Net;

namespace SecureCode.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException(string message) : base(message, null, HttpStatusCode.InternalServerError)
        {
        }
    }
}
