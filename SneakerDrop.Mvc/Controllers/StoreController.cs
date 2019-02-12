using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SneakerDrop.Mvc.Models;


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
            ViewBag.ProductName = productinfo.ProductTitle;
           return RedirectToAction("Catalog", "Home", ViewBag.ProductName);
        }
        [HttpPost]
        [ActionName("buyer2")]
        public IActionResult buyeritem(FindProductInfoViewModel productinfo)
        {
            return RedirectToAction("SingleItem", "Home", productinfo.ProductTitle);
        }
       }
    }

