using System;
using System.Collections.Generic;
using System.Linq;
using dm = SneakerDrop.Domain.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SneakerDrop.Mvc.Models;
using c = SneakerDrop.Code;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


namespace SneakerDrop.Mvc.Controllers
{
    public class StoreController : Controller
    {
        [HttpPost]
        [ActionName("seller")]
        public IActionResult SearchAction(string SearchAction)
        {
            switch (SearchAction)
            {
                case "Search":
                    return View("~/Views/Partials/ListingSearch.cshtml");
                case "Next":
                    return View("~/Views/Store/Listing.cshtml");
                default:
                    return View();
            }
        }
        [HttpPost]
        [ActionName("seller2")]
        public IActionResult ListingCheck(FindProductInfoViewModel productinfo)
        {

            ViewBag.Product = productinfo.ProductTitle;

            var listingcheck = new FindProductInfoViewModel();

            var newcheck = listingcheck.FindMatchingProductInfo(productinfo).FirstOrDefault();

            var viewcheck = new ConversionProduct();

             if (newcheck != null )
                {
                return View("~/Views/Store/Listing.cshtml");
                }
                return View("~/Views/Store/SellerSearch.cshtml");
            }

        [HttpPost]
        [ActionName("buyer")]
        public IActionResult BuyerSearch(FindProductInfoViewModel productinfo)
        {
            var productsearch = productinfo.FindMatchingProductInfo(productinfo).FirstOrDefault();

            if (productsearch != null)
            {
                HttpContext.Session.SetString("ProductName", productinfo.ProductTitle);
                return RedirectToAction("buyer2", "Store");
            }
            return RedirectToAction("Catalog", "Home");
           
        }

        [HttpGet]
        [ActionName("buyer2")]
        public IActionResult BuyerSearchPull()
        {
            var sessionproduct = HttpContext.Session.GetString("ProductName");

            var productdata = new FindProductInfoViewModel
            {
                ProductTitle = sessionproduct
            };
            return RedirectToAction("Catalog", "Home", productdata);
        }


        [HttpPost]
        [ActionName("buyer3")]
        public IActionResult buyeritem(FindProductInfoViewModel productinfo)
        {
            return RedirectToAction("SingleItem", "Home", productinfo.ProductTitle);
        }

        [HttpPost]
        [ActionName("orderinitial")]
        public IActionResult OrderInitial(FindProductInfoViewModel productinfo)
        {
            return RedirectToAction("Order", "Home", productinfo.ProductTitle);
        }
       }
    }

