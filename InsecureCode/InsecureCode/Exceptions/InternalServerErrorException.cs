using System.Net;

namespace InsecureCode.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException(string message) : base(message, null, HttpStatusCode.InternalServerError)
        {
        }
    }
}
