﻿using BooksManager.Application.Interfaces;
using BooksManager.Application.ViewModels;
using BooksManager.Domain.Exception;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BooksManager.Services.ApiBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ApiController
    {
        private readonly IBookAppService _bookAppService;

        public BookController(
            IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
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
                var bookViewModelResult = await _bookAppService.GetByIdAsync(id);

                if (bookViewModelResult?.Data == null) return NotFound(new { message = "A problem occurred during getting the data." });

                return Ok(bookViewModelResult.Data);
            }
            catch (ExceptionHandler exceptionHandler)
            {
                return ResponseByHttpStatusCode(exceptionHandler);
            }
        }

        /// <summary>
        /// Método responsável por registrar log com detalhes da homologação
        /// </summary>
        /// <param name="bookViewModel"></param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Result</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BookViewModel bookViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(new { message = "Invalid model state." });

                var bookViewModelResult = await _bookAppService.AddAsync(bookViewModel);

                if (bookViewModelResult?.Data == null) return BadRequest(new { message = "A problem occurred during adding the data." });

                var uri = Url.Action("Get", new { id = bookViewModelResult.Data.Id });

                return Created(uri, bookViewModelResult.Data);
            }
            catch (ExceptionHandler exceptionHandler)
            {
                return ResponseByHttpStatusCode(exceptionHandler);
            }
        }

        /// <summary>
        /// Método responsável por alterar log com detalhes da homologação
        /// </summary>
        /// <param name="bookViewModel"></param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>Result</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]BookViewModel bookViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var bookViewModelResult = await _bookAppService.UpdateAsync(bookViewModel);

                if (bookViewModelResult?.Data == null) return BadRequest(new { message = "A problem occurred during updating the data." });

                return Ok(bookViewModelResult.Data);
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
                await _bookAppService.RemoveAsync(id);

                return NoContent();
            }
            catch (ExceptionHandler exceptionHandler)
            {
                return ResponseByHttpStatusCode(exceptionHandler);
            }
        }
    }
}
