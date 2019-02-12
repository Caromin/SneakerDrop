﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SneakerDrop.Mvc.Models;
using c = SneakerDrop.Code.Helpers;
using dm = SneakerDrop.Domain.Models;

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
            var sessionusername = HttpContext.Session.GetString("Username");

            ViewBag.Username = sessionusername;
            if (ViewBag.Username != null)
            {
                return View("~/Views/User/Account.cshtml");
            }
            return View("~/Views/Home/Login.cshtml");
        }

        public IActionResult SellerSearch()
        {
            return View("~/Views/Store/SellerSearch.cshtml");
        }

        public IActionResult Listing()
        {
            return View("~/Views/Store/Listing.cshtml");
        }

        public IActionResult ChangeUserInfo()
        {
            return View("~/Views/User/ChangeUserInfo.cshtml");
        }


        public IActionResult OrderHistory()
        {
            return View("~/Views/User/OrderHistory.cshtml");
        }

        public IActionResult ChangeAddress()
        {
            return View("/Views/Partials/ChangeAddress.cshtml");
        }

        public IActionResult ChangePayment()
        {
            return View("/Views/Partials/ChangePayment.cshtml");
        }

        public IActionResult ChangeUser()
        {
            return View("/Views/Partials/ChangeUser.cshtml");
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
