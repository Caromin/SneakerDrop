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
       }
    }

