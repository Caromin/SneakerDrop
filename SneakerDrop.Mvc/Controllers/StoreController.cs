using System;
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
using Microsoft.AspNetCore.Session;


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
            int listingId = Int32.Parse(buy);

            List<int> ListingIdList = new List<int>();

            ListingIdList.Add(listingId);

            HttpContext.Session.SetString("ListOfIds", JsonConvert.SerializeObject(ListingIdList));
            //var PriceHelper = new FindProductInfoViewModel();

            //StaticCartViewModel.CartOfListId.Add(listingId);
            ////PriceHelper.HelperType = "buy";
            ////StaticCartViewModel.TotalPrice(PriceHelper);


            return RedirectToAction("Product", "Store");
        }

        [HttpGet]
        [ActionName("CartPull")]
        public IActionResult CartInfo()
        {
            c.SneakerDropDbContext db = new c.SneakerDropDbContext();
            var getIdList = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("ListOfIds"));

            foreach (var item in getIdList)
            {
                var cartstuff = db.Listings.Where(l => l.ListingId == item).ToList();
                var cartstufffirst = cartstuff.FirstOrDefault();
                foreach (var item2 in cartstuff)
                {
                    var cartstuff2 = db.ProductInfos.Where(p => p.ProductInfoId == item2.ProductInfoId).FirstOrDefault();
                    var producttime = new CreateNewListingViewModel
                    {
                        ProductTitle = cartstuff2.ProductTitle,
                        ImageUrl = cartstuff2.ImageUrl,
                        UserSetPrice = cartstufffirst.UserSetPrice,
                        Quantity = 1

                    };
                    StaticCartViewModel.CartOfProducts.Add(producttime);
                    HttpContext.Session.SetString("ProductTime", JsonConvert.SerializeObject(producttime));
                }

            }

            return RedirectToAction("CartPull2", "Store");

        }

        [HttpGet]
        [ActionName("CartPull2")]
        public IActionResult CartInfoPull()
        {
            var getProduct = JsonConvert.DeserializeObject<CreateNewListingViewModel>(HttpContext.Session.GetString("ProductTime"));
            return RedirectToAction("Cart", "Home", getProduct);
            
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

      

    }
}

