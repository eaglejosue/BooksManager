using Microsoft.AspNetCore.Mvc;
using BooksManager.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using BooksManager.Domain.Exception;
using BooksManager.Application.ViewModels;

namespace BooksManager.Presentation.Site.Controllers
{
    //[Authorize]
    //[Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookAppService _bookAppService;

        public BookController(
            IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customer-management/list-all")]
        public async Task<IActionResult> Index()
        {
            var allBookViewModelsResult = await _bookAppService.GetAllAsync();

            if (allBookViewModelsResult?.Data == null) return NotFound();

            return View(allBookViewModelsResult.Data);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customer-management/customer-details/{id:long}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();

            var bookViewModelResult = await _bookAppService.GetByIdAsync(id.Value);

            if (bookViewModelResult?.Data == null) return NotFound();

            return View(bookViewModelResult.Data);
        }

        [HttpGet]
        ////[Authorize(Policy = "CanWriteCustomerData")]
        [Route("customer-management/register-new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        ////[Authorize(Policy = "CanWriteCustomerData")]
        [Route("customer-management/register-new")]
        public async Task<IActionResult> Create(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid) return View(bookViewModel);

            var bookViewModelResult = await _bookAppService.AddAsync(bookViewModel);

            if (bookViewModelResult?.Data == null) return NotFound();

            return View(bookViewModelResult?.Data);
        }

        [HttpGet]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [Route("customer-management/edit-customer/{id:long}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var bookViewModelResult = await _bookAppService.GetByIdAsync(id.Value);

            if (bookViewModelResult?.Data == null) return NotFound();

            return View(bookViewModelResult.Data);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [Route("customer-management/edit-customer/{id:long}")]
        public async Task<IActionResult> Edit(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid) return View(bookViewModel);

            await _bookAppService.UpdateAsync(bookViewModel);

            ViewBag.Sucesso = "Customer Updated!";

            return View(bookViewModel);
        }

        [HttpGet]
        //[Authorize(Policy = "CanRemoveCustomerData")]
        [Route("customer-management/remove-customer/{id:long}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            var bookViewModelResult = await _bookAppService.GetByIdAsync(id.Value);

            if (bookViewModelResult?.Data == null) return NotFound();

            return View(bookViewModelResult.Data);
        }

        [HttpPost, ActionName("Delete")]
        //[Authorize(Policy = "CanRemoveCustomerData")]
        [Route("customer-management/remove-customer/{id:long}")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await _bookAppService.RemoveAsync(id);

                ViewBag.Sucesso = "Customer Removed!";

                return RedirectToAction("Index");
            }
            catch (ExceptionHandler)
            {
                return View(await _bookAppService.GetByIdAsync(id));
            }
        }
    }
}
