using BooksManager.Application.Interfaces;
using BooksManager.Application.ViewModels;
using BooksManager.Domain.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BooksManager.Presentation.Site.Controllers
{
    ////[Authorize]
    //[Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(
            ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customer-management/list-all")]
        public async Task<IActionResult> Index()
        {
            var allCustomerViewModelResult = await _customerAppService.GetAllAsync();

            if (allCustomerViewModelResult?.Data == null) return NotFound();

            return View(allCustomerViewModelResult.Data);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customer-management/customer-details/{id:guid}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();

            var customerViewModelResult = await _customerAppService.GetByIdAsync(id.Value);

            if (customerViewModelResult?.Data == null) return NotFound();

            return View(customerViewModelResult.Data);
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
        ////[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return View(customerViewModel);

            var customerViewModelResult = await _customerAppService.AddAsync(customerViewModel);

            if (customerViewModelResult?.Data == null) return NotFound();

            return View(customerViewModelResult?.Data);
        }

        [HttpGet]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [Route("customer-management/edit-customer/{id:guid}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var customerViewModelResult = await _customerAppService.GetByIdAsync(id.Value);

            if (customerViewModelResult?.Data == null) return NotFound();

            return View(customerViewModelResult.Data);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [Route("customer-management/edit-customer/{id:guid}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return View(customerViewModel);

            await _customerAppService.UpdateAsync(customerViewModel);

            ViewBag.Sucesso = "Customer Updated!";

            return View(customerViewModel);
        }

        [HttpGet]
        //[Authorize(Policy = "CanRemoveCustomerData")]
        [Route("customer-management/remove-customer/{id:guid}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            var customerViewModelResult = await _customerAppService.GetByIdAsync(id.Value);

            if (customerViewModelResult?.Data == null) return NotFound();

            return View(customerViewModelResult.Data);
        }

        [HttpPost, ActionName("Delete")]
        //[Authorize(Policy = "CanRemoveCustomerData")]
        [Route("customer-management/remove-customer/{id:guid}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await _customerAppService.RemoveAsync(id);

                ViewBag.Sucesso = "Customer Removed!";

                return RedirectToAction("Index");
            }
            catch (ExceptionHandler)
            {
                return View(_customerAppService.GetByIdAsync(id));
            }
        }
    }
}
