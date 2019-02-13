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
using cd = SneakerDrop.Code;
using SneakerDrop.Mvc.Models;
using c = SneakerDrop.Code.Helpers;
using dm = SneakerDrop.Domain.Models;
using System.Text.RegularExpressions;
using SneakerDrop.Code.Helpers;

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
            cd.SneakerDropDbContext db = new cd.SneakerDropDbContext();

            var sessionproduct = HttpContext.Session.GetString("ProductName");

            var createcatalog = db.ProductInfos.Where(p => p.ProductTitle.Contains(sessionproduct)).ToList();
              foreach(var item in createcatalog)
            { 
               KeyValuePair<string, object> catalogcreate = new KeyValuePair<string, object>(item.ProductTitle, item.ImageUrl);
                ViewData.Add(catalogcreate);

           }

            return View("~/Views/Store/Catalog.cshtml");
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

            return View("/Views/Partials/ChangeAddress.cshtml", list);
        }

        public IActionResult ChangePayment()
        {
            var getPayment = new PaymentViewModel
            {
                UserId = (int)HttpContext.Session.GetInt32("UserId")
            };
            List<PaymentViewModel> list = getPayment.GetAllPayments(getPayment);

            return View("/Views/Partials/ChangePayment.cshtml", list);
        }

        public IActionResult ChangeUser()
        {
            return View("/Views/Partials/ChangeUser.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddPayment(string payment)
        {
            string pattern1 = @"delete";
            string pattern2 = @"add";
            string validate = payment;
            Match match = Regex.Match(validate, pattern1);
            var paymentId = Regex.Match(payment, @"\d+").Value;
            string helperType = Regex.Match(payment, pattern2).Value;
            HttpContext.Session.SetString("HelperType", helperType);
            HttpContext.Session.SetString("PaymentId", paymentId);

            if (match.Success)
            {
                var result = Int32.Parse(paymentId);
                dm.Payment paymentInfo = new dm.Payment
                {
                    PaymentId = result
                };
                PaymentHelper.DeletePaymentByPaymentId(paymentInfo);

                return RedirectToAction("ChangePayment", "Home");
            }
            return RedirectToAction("AddPaymentView", "Home");
        }

        public IActionResult AddPaymentView()
        {
            return View("~/Views/User/AddPayment.cshtml");
        }

        public IActionResult CompletePayment(PaymentViewModel payment)
        {
            var sessionHelper = HttpContext.Session.GetString("HelperType");
            var sessionAddressId = HttpContext.Session.GetString("PaymentId");
            var sessionUserId = HttpContext.Session.GetInt32("UserId");
            int userId = (int)sessionUserId;

            var newModel = new PaymentViewModel
            {
                HelperType = sessionHelper,
                UserId = userId,
                CCNumber = payment.CCNumber,
                CCUserName = payment.CCUserName,
                Month = payment.Month,
                Year = payment.Year,
                CVV = payment.CVV
            };
            newModel.AddOrDeletePayments(newModel);

            return RedirectToAction("ChangePayment", "Home");
        }

        public IActionResult AddEditAddress(string address)
        {
            string pattern1 = @"delete";
            string pattern2 = @"add";
            string validate = address;
            Match match = Regex.Match(validate, pattern1);
            var addressId = Regex.Match(address, @"\d+").Value;
            string helperType = Regex.Match(address, pattern2).Value;
            HttpContext.Session.SetString("HelperType", helperType);
            HttpContext.Session.SetString("AddressId", addressId);

            if (match.Success)
            {
                var result = Int32.Parse(addressId);
                dm.Address addressInfo = new dm.Address
                {
                    AddressId = result
                };
                AddressHelper.DeleteAddressInfoById(addressInfo);

                return RedirectToAction("ChangeAddress", "Home");
            }

            return RedirectToAction("AddEditView", "Home");
        }

        public IActionResult AddEditView(string match)
        {
            return View("~/Views/User/AddEditAddress.cshtml");
        }

        public IActionResult AddEditInfo(AddressViewModel address)
        {
            var sessionHelper = HttpContext.Session.GetString("HelperType");
            var sessionAddressId = HttpContext.Session.GetString("AddressId");
            var sessionUserId = HttpContext.Session.GetInt32("UserId");
            int userId = (int)sessionUserId;

            if (sessionHelper == "add")
            {
                var newModel = new AddressViewModel
                {
                    HelperType = sessionHelper,
                    UserId = userId,
                    Street = address.Street,
                    City = address.City,
                    State = address.State,
                    PostalCode = address.PostalCode
                };

                newModel.AddEditDeleteAddresses(newModel);

                return RedirectToAction("ChangeAddress", "Home");
            }

            var newModel2 = new AddressViewModel
            {
                AddressId = Int32.Parse(sessionAddressId),
                HelperType = "edit",
                UserId = (int)sessionUserId,
                Street = address.Street,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode
            };

            newModel2.AddEditDeleteAddresses(newModel2);

            return RedirectToAction("ChangeAddress", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
