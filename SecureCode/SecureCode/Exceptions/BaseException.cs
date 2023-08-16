using System.Net;

namespace SecureCode.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message, List<string>? messages = default, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError) : base(message)
        {
            ErrorMessages = messages;
            ErrorCode = httpStatusCode;
        }
        public List<string>? ErrorMessages { get; set; }
        public HttpStatusCode ErrorCode { get; set; }
    }
}
