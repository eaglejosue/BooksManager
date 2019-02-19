using BooksManager.Application.Interfaces;
using BooksManager.Application.ViewModels;
using BooksManager.Domain.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksManager.Presentation.Web.Controllers
{
    //[Authorize]
    //[Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingAppService _bookingAppService;

        public BookingController(
            IBookingAppService bookingAppService)
        {
            _bookingAppService = bookingAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("booking-management/list-all")]
        public IActionResult Index()
        {
            var allBookingViewModelsResult = _bookingAppService.GetAll();

            if (allBookingViewModelsResult?.Data == null) return NotFound();

            return View(allBookingViewModelsResult.Data);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("booking-management/booking-details/{id:long}")]
        public IActionResult Details(long? id)
        {
            if (id == null) return NotFound();

            var bookingViewModelResult = _bookingAppService.GetById(id.Value);

            if (bookingViewModelResult?.Data == null) return NotFound();

            return View(bookingViewModelResult.Data);
        }

        [HttpGet]
        ////[Authorize(Policy = "CanWriteCustomerData")]
        [Route("booking-management/register-new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        ////[Authorize(Policy = "CanWriteCustomerData")]
        [Route("booking-management/register-new")]
        public IActionResult Create(BookingViewModel bookingViewModel)
        {
            if (!ModelState.IsValid) return View(bookingViewModel);

            var bookingViewModelResult = _bookingAppService.Add(bookingViewModel);

            if (bookingViewModelResult?.Data == null) return NotFound();

            return View(bookingViewModelResult.Data);
        }

        [HttpGet]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [Route("booking-management/edit-booking/{id:long}")]
        public IActionResult Edit(long? id)
        {
            if (id == null) return NotFound();

            var bookingViewModelResult = _bookingAppService.GetById(id.Value);

            if (bookingViewModelResult?.Data == null) return NotFound();

            return View(bookingViewModelResult.Data);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [Route("booking-management/edit-booking/{id:long}")]
        public IActionResult Edit(BookingViewModel bookingViewModel)
        {
            if (!ModelState.IsValid) return View(bookingViewModel);

            _bookingAppService.Update(bookingViewModel);

            ViewBag.Sucesso = "Booking Updated!";

            return View(bookingViewModel);
        }

        [HttpGet]
        //[Authorize(Policy = "CanRemoveCustomerData")]
        [Route("booking-management/remove-booking/{id:long}")]
        public IActionResult Delete(long? id)
        {
            if (id == null) return NotFound();

            var bookViewModelResult = _bookingAppService.GetById(id.Value);

            if (bookViewModelResult?.Data == null) return NotFound();

            return View(bookViewModelResult.Data);
        }

        [HttpPost, ActionName("Delete")]
        //[Authorize(Policy = "CanRemoveCustomerData")]
        [Route("booking-management/remove-booking/{id:long}")]
        public IActionResult DeleteConfirmed(long id)
        {
            try
            {
                _bookingAppService.Remove(id);

                ViewBag.Sucesso = "Booking Removed!";

                return RedirectToAction("Index");
            }
            catch (ExceptionHandler)
            {
                return View(_bookingAppService.GetById(id).Data);
            }
        }
    }
}
