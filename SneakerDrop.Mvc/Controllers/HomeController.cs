﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SneakerDrop.Mvc.Models;

namespace SneakerDrop.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Account()
        {
            return View("~/Views/User/Account.cshtml");
        }

        public IActionResult Listing()
        {
            return View("~/Views/User/Listing.cshtml");
        }

        public IActionResult ChangeUserInfo()
        {
            return View("~/Views/User/ChangeUserInfo.cshtml");
        }

        public IActionResult OrderHistory()
        {
            return View("~/Views/User/OrderHistory.cshtml");
        }

        public IActionResult ChangePayment()
        {
            return View("~/Views/User/ChangePayment.cshtml");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
