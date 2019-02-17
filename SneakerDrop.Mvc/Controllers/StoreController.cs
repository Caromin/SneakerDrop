﻿using System;
using System.Collections.Generic;
using System.Linq;
using dm = SneakerDrop.Domain.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SneakerDrop.Mvc.Models;
using c = SneakerDrop.Code;
using SneakerDrop.Code.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace SneakerDrop.Mvc.Controllers
{
    public class StoreController : Controller
    {

        public static List<int> ListOfIds { get; set; }

        public static List<OrderAndPaymentViewModel> ListOfProducts { get; set; }

        static StoreController()
        {
            ListOfIds = new List<int>();
            ListOfProducts = new List<OrderAndPaymentViewModel>();
        }

        [HttpPost]
        [ActionName("seller2")]
        public IActionResult ListingCheck(FindProductInfoViewModel productinfo)
        {

            ViewBag.Product = productinfo.ProductTitle;

            var listingcheck = new FindProductInfoViewModel();

            var newcheck = listingcheck.FindMatchingProductInfo(productinfo).FirstOrDefault();

            var viewcheck = new ConversionProduct();

            if (newcheck != null)
            {
                return View("~/Views/Store/Listing.cshtml");
            }
            return View("~/Views/Store/SellerSearch.cshtml");
        }

        [HttpPost]
        [ActionName("buyer")]
        public IActionResult BuyerSearch(List<FindProductInfoViewModel> findProductInfos, string sell)
        {
            c.SneakerDropDbContext db = new c.SneakerDropDbContext();
            if (sell != null)
            {
                HttpContext.Session.SetString("Selling", sell);
            }

            foreach (var item in findProductInfos)
            {
                string PTitle = item.ProductTitle;
                HttpContext.Session.SetString("ProductName", PTitle);
            }

            if (sell == "seller")
            {
                return RedirectToAction("SellCatalog", "Home");
            }
            return RedirectToAction("Catalog", "Home");

        }

        [HttpPost]
        [ActionName("buyer3")]
        public IActionResult buyeritem(FindProductInfoViewModel productinfo)
        {
            return RedirectToAction("SingleItem", "Home", productinfo.ProductTitle);
        }

        [HttpPost]
        [ActionName("orderinitial")]
        public IActionResult OrderInitial(string buy)
        {
            //first in route of Cart
            int listingId = Int32.Parse(buy);
            HttpContext.Session.SetInt32("nodup", listingId);

            ListOfIds.Insert(0, listingId);

            //serializes the List into a Json object
            HttpContext.Session.SetString("ListOfIds", JsonConvert.SerializeObject(ListOfIds));

            return RedirectToAction("CartPull", "Store");
        }

        [HttpGet]
        [ActionName("CartPull")]
        public IActionResult CartInfo()
        {

            //second in route of cart
            c.SneakerDropDbContext _db = new c.SneakerDropDbContext();

            //retrieves the Json object and deserializes into a list of ListingIds
            var getIdList = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("ListOfIds"));
            var getnodup = HttpContext.Session.GetInt32("nodup");

            dm.Listing results = _db.Listings.Where(l => l.ListingId == getIdList[0]).Include(l => l.ProductInfo).FirstOrDefault();
            OrderAndPaymentViewModel model = new OrderAndPaymentViewModel
            {
                ListingId = getIdList[0],
                Quantity = results.Quantity,
                ProductInfoId = results.ProductInfoId,
                UserSetPrice = (decimal)results.UserSetPrice,
                Size = results.Size,
                ProductTitle = results.ProductInfo.ProductTitle,
                Color = results.ProductInfo.Color,
                ImageUrl = results.ProductInfo.ImageUrl
            };

            ListOfProducts.Add(model);
            HttpContext.Session.SetString("ProductTime", JsonConvert.SerializeObject(ListOfProducts));

            return RedirectToAction("Cart", "Home");
        }




        [HttpGet]
        [ActionName("Product")]
        public IActionResult ProductInfoView(string viewItem)
        {
            int selectedProductId;

            if (viewItem == null)
            {
                selectedProductId = Int32.Parse(HttpContext.Session.GetString("ProductId"));
            }
            else
            {
                HttpContext.Session.SetString("ProductId", viewItem);
                selectedProductId = Int32.Parse(viewItem);
            }

            var convert = new ConversionListing();

            List<dm.Listing> allListings = ListingHelper.GetAllListingsByProductInfoId(selectedProductId);
            dm.ProductInfo productInfo = FindProductInfoHelper.SingleProductById(selectedProductId);

            List<SingleProductViewModel> convertedList = convert.MappingAllViewListings(allListings);

            foreach (var item in convertedList)
            {
                item.Color = productInfo.Color;
                item.Description = productInfo.Description;
                item.ImageUrl = productInfo.ImageUrl;
                item.DisplayPrice = productInfo.DisplayPrice;
                item.ReleaseDate = productInfo.ReleaseDate;
                item.ProductTitle = productInfo.ProductTitle;
            }

            if (convertedList.Count == 0)
            {
                convertedList.Add(new SingleProductViewModel
                {
                    Color = productInfo.Color,
                    Description = productInfo.Description,
                    ImageUrl = productInfo.ImageUrl,
                    DisplayPrice = productInfo.DisplayPrice,
                    ReleaseDate = productInfo.ReleaseDate,
                    ProductTitle = productInfo.ProductTitle
                });
            }


            return View("~/Views/Store/SingleItem.cshtml", convertedList);
        }


        [HttpPost]
        [ActionName("CreateListing")]
        public IActionResult CreateListing(CreateNewListingViewModel passedInfo)
        {
            var model = new CreateNewListingViewModel();
            var id = HttpContext.Session.GetInt32("SellingProductId");
            dm.ProductInfo productInfo = FindProductInfoHelper.SingleProductById((int)id);

            var listing = new dm.Listing
            {
                UserSetPrice = passedInfo.UserSetPrice,
                Quantity = passedInfo.Quantity,
                Size = passedInfo.Size,
                User = new dm.User
                {
                    UserId = (int)HttpContext.Session.GetInt32("UserId")
                },
                ProductInfo = new dm.ProductInfo
                {
                    ProductInfoId = (int)HttpContext.Session.GetInt32("SellingProductId")
                }
            };
            model.AddListing(listing);

            return RedirectToAction("Account", "Home");
        }

        [HttpGet]
        [ActionName("OrderProcess")]
        public IActionResult OrderProcess()
        {
            
            var getAddress = new AddressViewModel
            {
                UserId = (int)HttpContext.Session.GetInt32("UserId")
            };

            var sessionusername = HttpContext.Session.GetString("Username");
            ViewBag.Username = sessionusername;

            List<AddressViewModel> list = getAddress.GetAllAddresses(getAddress);
            var viewaddress = list.FirstOrDefault();

            if (list.Count == 0)
            {
               RedirectToAction("ChangeAddress", "Home");
            }
            ViewBag.Street = viewaddress.Street;
            ViewBag.City = viewaddress.City;
            ViewBag.State = viewaddress.State;
            ViewBag.PostalCode = viewaddress.PostalCode;

            var getPayment = new PaymentViewModel
            {
                UserId = (int)HttpContext.Session.GetInt32("UserId")
            };
            List<PaymentViewModel> list2 = getPayment.GetAllPayments(getPayment);
            var viewpayment = list2.FirstOrDefault();

            if (list2.Count == 0)
            {
                RedirectToAction("ChangePayment", "Home");
            }

            HttpContext.Session.SetInt32("addressid", viewpayment.PaymentId);
            ViewBag.CardNumber = viewpayment.CCNumber;
            ViewBag.Expiration = $"{viewpayment.Month}/{viewpayment.Year}";
            ViewBag.CardName = viewpayment.CCUserName;

           var GetTotalPrice = HttpContext.Session.GetString("totalprice");
            ViewBag.TotalPrice = GetTotalPrice;

            var getProduct = JsonConvert.DeserializeObject<List<OrderAndPaymentViewModel>>(HttpContext.Session.GetString("ProductTime"));

            return View("~/Views/Store/Order.cshtml", getProduct);
        }

      [HttpGet]
      [ActionName("OrderHistory")]
      public IActionResult OrderHistory()
        {
            return View();
        }
    }
}

