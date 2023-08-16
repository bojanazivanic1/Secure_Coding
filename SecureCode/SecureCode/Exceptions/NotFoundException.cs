using System.Net;

namespace SecureCode.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}
