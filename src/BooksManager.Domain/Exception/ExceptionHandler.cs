using System.Net;

namespace BooksManager.Domain.Exception
{
    public class ExceptionHandler : System.Exception
    {
        public HttpStatusCode HttpStatusCode { get; }
        public override string Message { get; }

        public ExceptionHandler(string message = null)
        {
            HttpStatusCode = HttpStatusCode.BadRequest;
            Message = message ?? string.Empty;
        }

        public ExceptionHandler(HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            HttpStatusCode = httpStatusCode;
            Message = string.Empty;
        }

        public ExceptionHandler(HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, string message = null)
        {
            HttpStatusCode = httpStatusCode;
            Message = message ?? string.Empty;
        }

        public ExceptionHandler(string message, HttpStatusCode httpStatusCode)
        {
            Message = message;
            HttpStatusCode = httpStatusCode;
        }
    }
}
