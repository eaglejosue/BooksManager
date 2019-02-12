﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            var allBookViewModelsResult = _bookAppService.GetAll();

            if (allBookViewModelsResult?.Data == null) return NotFound();

            return View(allBookViewModelsResult.Data);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customer-management/customer-details/{id:long}")]
        public IActionResult Details(long? id)
        {
            if (id == null) return NotFound();

            var bookViewModelResult = _bookAppService.GetById(id.Value);

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
        public IActionResult Create(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid) return View(bookViewModel);

            var bookViewModelResult = _bookAppService.Add(bookViewModel);

            if (bookViewModelResult?.Data == null) return NotFound();

            return View(bookViewModelResult?.Data);
        }

        [HttpGet]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [Route("customer-management/edit-customer/{id:long}")]
        public IActionResult Edit(long? id)
        {
            if (id == null) return NotFound();

            var bookViewModelResult = _bookAppService.GetById(id.Value);

            if (bookViewModelResult?.Data == null) return NotFound();

            return View(bookViewModelResult.Data);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [Route("customer-management/edit-customer/{id:long}")]
        public IActionResult Edit(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid) return View(bookViewModel);

            _bookAppService.Update(bookViewModel);

            ViewBag.Sucesso = "Customer Updated!";

            return View(bookViewModel);
        }

        [HttpGet]
        //[Authorize(Policy = "CanRemoveCustomerData")]
        [Route("customer-management/remove-customer/{id:long}")]
        public IActionResult Delete(long? id)
        {
            if (id == null) return NotFound();

            var bookViewModelResult = _bookAppService.GetById(id.Value);

            if (bookViewModelResult?.Data == null) return NotFound();

            return View(bookViewModelResult.Data);
        }

        [HttpPost, ActionName("Delete")]
        //[Authorize(Policy = "CanRemoveCustomerData")]
        [Route("customer-management/remove-customer/{id:long}")]
        public IActionResult DeleteConfirmed(long id)
        {
            try
            {
                _bookAppService.Remove(id);

                ViewBag.Sucesso = "Customer Removed!";

                return RedirectToAction("Index");
            }
            catch (ExceptionHandler)
            {
                return View(_bookAppService.GetById(id));
            }
        }
    }
}
