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


namespace SneakerDrop.Mvc.Controllers
{
    public class StoreController : Controller
    {

        public static List<int> ListOfIds { get; set; }

        public static List<dm.ProductInfo> ListOfProducts { get; set; }

        static StoreController()
        {
            ListOfIds = new List<int>();
            ListOfProducts = new List<dm.ProductInfo>();
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


            ListOfIds.Add(listingId);



            //serializes the List into a Json object
            HttpContext.Session.SetString("ListOfIds", JsonConvert.SerializeObject(ListOfIds));




            return RedirectToAction("CartPull", "Store");


        }

        [HttpGet]
        [ActionName("CartPull")]
        public IActionResult CartInfo()
        {

            //second in route of cart
            c.SneakerDropDbContext db = new c.SneakerDropDbContext();

            //retrieves the Json object and deserializes into a list of ListingIds
            var getIdList = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("ListOfIds"));
            var getnodup = HttpContext.Session.GetInt32("nodup");

            foreach (var item in getIdList)
            {
                if (item == getnodup)
                {
                    var cartstuff = db.Listings.Where(l => l.ListingId == item).ToList();
                    HttpContext.Session.SetString("ListingTime", JsonConvert.SerializeObject(cartstuff));
                    var cartstufffirst = cartstuff.FirstOrDefault();
                    foreach (var item2 in cartstuff)
                    {
                        decimal oriprice = item2.UserSetPrice;
                        int newprice = Convert.ToInt32(oriprice);
                        
          
                        HttpContext.Session.SetString("transferprice", JsonConvert.SerializeObject(newprice));
                       
                        var cartstuff2 = db.ProductInfos.Where(p => p.ProductInfoId == item2.ProductInfoId).FirstOrDefault();
                        cartstuff2.DisplayPrice = newprice;
                        ListOfProducts.Add(cartstuff2);
                        
                        HttpContext.Session.SetString("ProductTime", JsonConvert.SerializeObject(ListOfProducts));
                    }

                }
            }
            
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
    }
}

