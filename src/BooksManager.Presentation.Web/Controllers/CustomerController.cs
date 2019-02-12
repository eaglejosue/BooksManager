using BooksManager.Application.Interfaces;
using BooksManager.Application.ViewModels;
using BooksManager.Domain.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksManager.Presentation.Web.Controllers
{
    //[Authorize]
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
        public IActionResult Index()
        {
            var allCustomerViewModelResult = _customerAppService.GetAll();

            if (allCustomerViewModelResult?.Data == null) return NotFound();

            return View(allCustomerViewModelResult.Data);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customer-management/customer-details/{id:long}")]
        public IActionResult Details(long? id)
        {
            if (id == null) return NotFound();

            var customerViewModelResult = _customerAppService.GetById(id.Value);

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
        public IActionResult Create(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return View(customerViewModel);

            var customerViewModelResult = _customerAppService.Add(customerViewModel);

            if (customerViewModelResult?.Data == null) return NotFound();

            return View(customerViewModelResult.Data);
        }

        [HttpGet]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [Route("customer-management/edit-customer/{id:long}")]
        public IActionResult Edit(long? id)
        {
            if (id == null) return NotFound();

            var customerViewModelResult = _customerAppService.GetById(id.Value);

            if (customerViewModelResult?.Data == null) return NotFound();

            return View(customerViewModelResult.Data);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [Route("customer-management/edit-customer/{id:long}")]
        public IActionResult Edit(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return View(customerViewModel);

            _customerAppService.Update(customerViewModel);

            ViewBag.Sucesso = "Customer Updated!";

            return View(customerViewModel);
        }

        [HttpGet]
        //[Authorize(Policy = "CanRemoveCustomerData")]
        [Route("customer-management/remove-customer/{id:long}")]
        public IActionResult Delete(long? id)
        {
            if (id == null) return NotFound();

            var customerViewModelResult = _customerAppService.GetById(id.Value);

            if (customerViewModelResult?.Data == null) return NotFound();

            return View(customerViewModelResult.Data);
        }

        [HttpPost, ActionName("Delete")]
        //[Authorize(Policy = "CanRemoveCustomerData")]
        [Route("customer-management/remove-customer/{id:long}")]
        public IActionResult DeleteConfirmed(long id)
        {
            try
            {
                _customerAppService.Remove(id);

                ViewBag.Sucesso = "Customer Removed!";

                return RedirectToAction("Index");
            }
            catch (ExceptionHandler)
            {
                return View(_customerAppService.GetById(id).Data);
            }
        }
    }
}
