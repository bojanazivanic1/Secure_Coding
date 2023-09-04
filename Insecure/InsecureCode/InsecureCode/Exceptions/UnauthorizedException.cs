using System.Net;

namespace InsecureCode.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message) : base(message, null, HttpStatusCode.Unauthorized)
        {
        }
    }
}
