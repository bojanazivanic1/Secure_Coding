using Newtonsoft.Json;
using System.Net;

namespace InsecureCode.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                string errorId = Guid.NewGuid().ToString();

                ErrorResult errorResult = new ErrorResult
                {
                    ErrorId = errorId,
                    Source = exception.TargetSite?.DeclaringType?.FullName,
                    Exception = exception.Message.Trim(),
                    SupportMessage = "Error ID: " + errorId
                };
                
                errorResult.Messages.Add(exception.Message);

                if (exception is not BaseException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                switch (exception)
                {
                    case BaseException e:
                        errorResult.StatusCode = (int)e.ErrorCode;
                        if(e.ErrorMessages is not null)
                        {
                            errorResult.Messages = e.ErrorMessages;
                        }
                        break;

                    case KeyNotFoundException:
                        errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                HttpResponse response = context.Response;

                if(!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = errorResult.StatusCode;
                    await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
                }
            }
        }
    }
}
