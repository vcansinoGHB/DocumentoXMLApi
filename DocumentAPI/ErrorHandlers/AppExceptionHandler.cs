using DocumentInfrastructure.ErrorHandlers;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace DocumentAPI.ErrorHandlers
{
    public class AppExceptionHandler: IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new ErrorResponse();
            switch (exception)
            {
                case InvalidTokenException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Title = exception.GetType().Name;
                    response.Detail = exception.Message ;
                    break;
                case InvalidXmlDocumentException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Title = exception.GetType().Name;
                    response.Detail = exception.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Title = "Internal Server Error";
                    response.Detail = $"Error is: {exception.Message}";
                    break;
            }

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;

        }
    }
}
