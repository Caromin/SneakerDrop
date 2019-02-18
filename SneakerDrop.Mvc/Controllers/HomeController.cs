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
        public static List<OrderAndPaymentViewModel> UpdatedList { get; set; }

        static HomeController()
        {
            UpdatedList = new List<OrderAndPaymentViewModel>();
        }

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

        public IActionResult Listing(string sellItem)
        {
            var productId = Int32.Parse(sellItem);
            HttpContext.Session.SetInt32("SellingProductId", productId);
            dm.ProductInfo domainModel = FindProductInfoHelper.SingleProductById(productId);
            var viewModel = new ConversionNewListing();
            CreateNewListingViewModel listing = viewModel.MappingCreateListing(domainModel);

            return View("~/Views/Store/Listing.cshtml", listing);
        }

        public IActionResult ChangeUserInfo()
        {
            var sessionuserid = HttpContext.Session.GetInt32("UserId");
            var userInfo = UserHelper.GetUserInfoById((int)sessionuserid);
            var model = new ConversionUser();
            UserViewModel viewModel = model.MappingViewInfo(userInfo);

            return View("~/Views/User/ChangeUserInfo.cshtml", viewModel);
        }


        public IActionResult Catalog()
        {
            cd.SneakerDropDbContext db = new cd.SneakerDropDbContext();
            var sessionproduct = HttpContext.Session.GetString("ProductName");
            var model = new FindProductInfoViewModel { ProductTitle = sessionproduct };
            List<FindProductInfoViewModel> results = model.FindMatchingProductInfo(model);
            var createcatalog2 = db.Type.Where(t => t.TypeName.Contains(sessionproduct)).ToList();
            List<FindProductInfoViewModel> onlyType = new List<FindProductInfoViewModel>();


            if (results == null)
            {
                foreach (var item1 in createcatalog2)
                {
                    var typeid = item1.TypeId;
                    var createcatalog3 = db.ProductInfos.Where(p => p.Type.TypeId == typeid).ToList();
                    var typeList = model.ConvertListOnly(createcatalog3);
                    onlyType.AddRange(typeList);
                }

                return View("~/Views/Store/Catalog.cshtml", onlyType);
            }
            foreach (var item2 in createcatalog2)
            {
                var typeid = item2.TypeId;
                var createcatalog3 = db.ProductInfos.Where(p => p.Type.TypeId == typeid).ToList();
                var typeList = model.ConvertListOnly(createcatalog3);
                results.AddRange(typeList);
            }

            // for if user inputs something that doesnt exist in db
            if (results.Count == 0 && createcatalog2.Count == 0)
            {
                return View("~/Views/Store/SellerCatalog.cshtml", onlyType);
            }

            return View("~/Views/Store/Catalog.cshtml", results);
        }

        public ActionResult SellCatalog()
        {
            cd.SneakerDropDbContext db = new cd.SneakerDropDbContext();
            var sessionproduct = HttpContext.Session.GetString("ProductName");
            var model = new FindProductInfoViewModel { ProductTitle = sessionproduct };
            List<FindProductInfoViewModel> results = model.FindMatchingProductInfo(model);
            var createcatalog2 = db.Type.Where(t => t.TypeName.Contains(sessionproduct)).ToList();
            List<FindProductInfoViewModel> onlyType = new List<FindProductInfoViewModel>();


            if (results == null)
            {
                foreach (var item1 in createcatalog2)
                {
                    var typeid = item1.TypeId;
                    var createcatalog3 = db.ProductInfos.Where(p => p.Type.TypeId == typeid).ToList();
                    var typeList = model.ConvertListOnly(createcatalog3);
                    onlyType.AddRange(typeList);
                }

                return View("~/Views/Store/SellerCatalog.cshtml", onlyType);
            }
            foreach (var item2 in createcatalog2)
            {
                var typeid = item2.TypeId;
                var createcatalog3 = db.ProductInfos.Where(p => p.Type.TypeId == typeid).ToList();
                var typeList = model.ConvertListOnly(createcatalog3);
                results.AddRange(typeList);
            }

            // for if user inputs something that doesnt exist in db
            if (results.Count == 0 && createcatalog2.Count == 0)
            {
                return View("~/Views/Store/SellerCatalog.cshtml", onlyType);
            }

            return View("~/Views/Store/SellerCatalog.cshtml", results);
        }

        public IActionResult SingleItem(FindProductInfoViewModel productinfo)
        {

            return View("~/Views/Store/SingleItem.cshtml");
        }

        public IActionResult Cart(string delete)
        {
            var nodup = HttpContext.Session.GetInt32("nodup");
            ViewBag.MessageGood = "You Have Added";
            int listingId = 0;
            decimal totalprice = 0;
            int deletehelper = 0;

            if (nodup == null)
            {
                ViewBag.MessageGood = "Empty";
                return View("~/Views/Store/Cart.cshtml");
            }

            if (delete == "checkout")
            {
                if (HttpContext.Session.GetInt32("UserId") == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                return RedirectToAction("OrderProcess", "Store");
            }

            if (delete != null)
            {
                deletehelper = 1;
                HttpContext.Session.SetInt32("deletehelper", deletehelper);

                listingId = Int32.Parse(Regex.Match(delete, @"\d+").Value);
                HttpContext.Session.SetInt32("listingiddelete", listingId);

                RedirectToAction("CartPull", "Store");

                //var getProduct = JsonConvert.DeserializeObject<List<OrderAndPaymentViewModel>>(HttpContext.Session.GetString("ProductTime"));

                // getProduct.RemoveAll(p => p.ListingId == listingId);

                //UpdatedList = getProduct;


                //return View("~/Views/Store/Cart.cshtml", UpdatedList);
            }



            //if (listingId == 0)
            //{
            //    if (UpdatedList.Count != 0)
            //    {
            //        foreach (var item in UpdatedList)
            //        {
            //            totalprice += (decimal)item.UserSetPrice;
            //        }
            //        ViewBag.Price = totalprice;

            //        return View("~/Views/Store/Cart.cshtml", UpdatedList);
            //    }

        
               var getProduct = JsonConvert.DeserializeObject<List<OrderAndPaymentViewModel>>(HttpContext.Session.GetString("ProductTime"));

                    foreach (var item in getProduct)
                    {
                        totalprice += (decimal)item.UserSetPrice;
                    }
                    ViewBag.Price = totalprice;
                    string TotalPrice = totalprice.ToString();
                   HttpContext.Session.SetString("totalprice", TotalPrice);

                    return View("~/Views/Store/Cart.cshtml", getProduct);
                
                
            
            //if (JsonConvert.DeserializeObject<List<OrderAndPaymentViewModel>>(HttpContext.Session.GetString("ProductTime")).Count == 0)
            //{
            //    List<OrderAndPaymentViewModel> test = new List<OrderAndPaymentViewModel>()
            //    {
            //        new OrderAndPaymentViewModel()
            //    };

            //    return View("~/Views/Store/Cart.cshtml", test);
            //}

            //var updatedList = JsonConvert.DeserializeObject<List<OrderAndPaymentViewModel>>(HttpContext.Session.GetString("ProductTime"));

            



            //return View("~/Views/Store/Cart.cshtml", updatedList);
        }



     
   
        public IActionResult Completion(string orderhistory)
        {
            if (orderhistory != null)
            {
                RedirectToAction("OrderHistory");
            }
            return View("~/Views/Store/Completion.cstml");
        
       }

        public IActionResult OrderHistory()
        {

        var sessionusername = (int)HttpContext.Session.GetInt32("UserId");

        


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

        public IActionResult AddEditView(AddressViewModel address)
        {
            var sessionUserId = HttpContext.Session.GetInt32("UserId");
            var addressInfo = AddressHelper.GetAddressInfoByAddressId((int)sessionUserId);
            var model = new ConversionAddress();
            AddressViewModel addressView = model.MappingAddressInfo(addressInfo);

            return View("~/Views/User/AddEditAddress.cshtml", addressView);
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

        public IActionResult EditUserInfo(UserViewModel user)
        {
            var sessionUserId = HttpContext.Session.GetInt32("UserId");

            var editedUser = new UserViewModel
            {
                UserId = (int)sessionUserId,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password
            };
            if (editedUser.AddEditUser(editedUser))
            {
                HttpContext.Session.SetString("Username", user.Username);
            }
            return RedirectToAction("Account", "Home");
        }

        public IActionResult GetListing()
        {
            List<dm.Listing> listings = ListingHelper.GetAllListingById((int)HttpContext.Session.GetInt32("UserId"));
            var model = new ConversionNewListing();
            List<CreateNewListingViewModel> viewModel = model.MappingViewListings(listings);

            foreach (var item in viewModel)
            {
                var productInfo = ListingHelper.GetProductIdByListingId(item.ListingId);
                item.ProductTitle = productInfo.ProductTitle;
                item.ImageUrl = productInfo.ImageUrl;
            }

            return View("~/Views/Partials/GetListings.cshtml", viewModel);
        }

        public IActionResult RemoveListing(string listing)
        {
            int listingId = Int32.Parse(Regex.Match(listing, @"\d+").Value);

            ListingHelper.DeleteListingById(listingId);

            return RedirectToAction("GetListing", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
