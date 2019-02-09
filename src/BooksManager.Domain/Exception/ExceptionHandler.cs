using System.Net;

namespace BooksManager.Domain.Exception
{
    public class ExceptionHandler : System.Exception
    {
        public HttpStatusCode HttpStatusCode { get; }
        public override string Message { get; }

        public ExceptionHandler(HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, string message = null)
        {
            HttpStatusCode = httpStatusCode;
            Message = message ?? string.Empty;
        }

        public ExceptionHandler() { }
    }
}
