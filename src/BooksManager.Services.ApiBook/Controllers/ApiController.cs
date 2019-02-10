using System.Net;
using Microsoft.AspNetCore.Mvc;
using BooksManager.Domain.Exception;

namespace BooksManager.Services.ApiBook.Controllers
{
    public class ApiController : ControllerBase
    {
        protected new IActionResult Response(object result = null)
        {
            if (ModelState.IsValid)
                return Ok(result);

            return BadRequest();
        }

        public IActionResult ResponseByHttpStatusCode(ExceptionHandler exceptionHandler)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (exceptionHandler.HttpStatusCode)
            {
                case HttpStatusCode.NotFound:
                    return NotFound(new { message = exceptionHandler.Message });

                case HttpStatusCode.Conflict:
                    return Conflict(new { message = exceptionHandler.Message });

                case HttpStatusCode.NoContent:
                    return NoContent();

                case HttpStatusCode.OK:
                    return Ok(new { message = exceptionHandler.Message });

                case HttpStatusCode.Unauthorized:
                    return Unauthorized(new { message = exceptionHandler.Message });

                default: return BadRequest(new { message = exceptionHandler.Message });
            }
        }
    }
}