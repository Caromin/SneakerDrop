using System;
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
            FindProductInfoViewModel createFindProductModel = new FindProductInfoViewModel();
            List<FindProductInfoViewModel> mostRecentList = createFindProductModel.FindMostRecentListings();

            return View(mostRecentList);
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
            var sessionuserid = HttpContext.Session.GetInt32("UserId");

            ViewBag.Username = sessionusername;
            ViewBag.UserId = sessionuserid;

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

        public IActionResult Catalog()
        {
            return View("~/Views/Store/Catalog.cshtml");
        }

        [HttpGet]
        [ActionName("Catalog")]
        public IActionResult CatalogSearch()
        {
            var sessionproduct = HttpContext.Session.GetString("ProductName");

            FindProductInfoViewModel createcatalog = new FindProductInfoViewModel();
            List<FindProductInfoViewModel> expandcatalog = createcatalog.SearchFind(sessionproduct);
            return View(expandcatalog);
        }


        public IActionResult SingleItem(FindProductInfoViewModel productinfo)
        {

            return View("~/Views/Store/SingleItem.cshtml");
        }

        
        public IActionResult Order(FindProductInfoViewModel productinfo)
        {
            return View("~/Views/Store/Order.cshtml");
        }


        public IActionResult OrderHistory()
        {
            return View("~/Views/User/OrderHistory.cshtml");
        }

        public IActionResult ChangeAddress()
        {
            var getAddress = new AddressViewModel
            {
                UserId = (int)HttpContext.Session.GetInt32("UserId")
            };

            List<AddressViewModel> list = getAddress.GetAllAddresses(getAddress);

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
