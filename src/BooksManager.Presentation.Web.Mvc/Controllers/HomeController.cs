﻿using System.Diagnostics;
using BooksManager.Presentation.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksManager.Presentation.Web.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        [Route("welcome")]
        [Route("")]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Route("access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
