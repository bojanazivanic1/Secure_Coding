using System.Net;

namespace SecureCode.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message) : base(message, null, HttpStatusCode.Unauthorized)
        {
        }
    }
}
