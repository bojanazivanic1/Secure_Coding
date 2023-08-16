using System.Net;

namespace InsecureCode.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}
