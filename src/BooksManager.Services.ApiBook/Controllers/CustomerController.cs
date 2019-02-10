using BooksManager.Application.Interfaces;
using BooksManager.Application.ViewModels;
using BooksManager.Domain.Exception;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BooksManager.Services.ApiBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(
            ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        /// <summary>
        /// Método responsável por obter log específico com detalhes da homologação
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Result</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var customerViewModelResult = await _customerAppService.GetByIdAsync(id);

                if (customerViewModelResult?.Data == null) return NotFound(new { message = "A problem occurred during getting the data." });

                return Ok(customerViewModelResult.Data);
            }
            catch (ExceptionHandler exceptionHandler)
            {
                return ResponseByHttpStatusCode(exceptionHandler);
            }
        }

        /// <summary>
        /// Método responsável por registrar log com detalhes da homologação
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Result</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerViewModel customerViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(new { message = "Invalid model state." });

                var customerViewModelResult = await _customerAppService.AddAsync(customerViewModel);

                if (customerViewModelResult?.Data == null) return BadRequest(new { message = "A problem occurred during adding the data." });

                var uri = Url.Action("Get", new { id = customerViewModelResult.Data.Id });

                return Created(uri, customerViewModelResult.Data);
            }
            catch (ExceptionHandler exceptionHandler)
            {
                return ResponseByHttpStatusCode(exceptionHandler);
            }
        }

        /// <summary>
        /// Método responsável por alterar log com detalhes da homologação
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Result</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]CustomerViewModel customerViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var customerViewModelResult = await _customerAppService.UpdateAsync(customerViewModel);

                if (customerViewModelResult?.Data == null) return BadRequest(new { message = "A problem occurred during updating the data." });

                return Ok(customerViewModelResult.Data);
            }
            catch (ExceptionHandler exceptionHandler)
            {
                return ResponseByHttpStatusCode(exceptionHandler);
            }
        }

        /// <summary>
        /// Método responsável por remover log específico de detalhes da homologação
        /// </summary>
        /// <param name="id"></param>
        /// <response code="203">NoContent</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _customerAppService.RemoveAsync(id);

                return NoContent();
            }
            catch (ExceptionHandler exceptionHandler)
            {
                return ResponseByHttpStatusCode(exceptionHandler);
            }
        }
    }
}
