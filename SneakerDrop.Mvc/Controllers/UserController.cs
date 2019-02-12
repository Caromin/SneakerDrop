using System;
using System.Collections.Generic;
using System.Linq;
using dm = SneakerDrop.Domain.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SneakerDrop.Mvc.Models;
using SneakerDrop.Code.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using c = SneakerDrop.Code;



namespace SneakerDrop.Mvc.Controllers
{
    public class UserController : Controller
    {
        
         [HttpPost]
        [ActionName("register")]
        public IActionResult RegisterFilter(UserViewModel userviewmodel)
        {
            ViewBag.Firstname = userviewmodel.Firstname;
            ViewBag.Lastname = userviewmodel.Lastname;
            ViewBag.Username = userviewmodel.Username;
            ViewBag.Password = userviewmodel.Password;
            ViewBag.Email = userviewmodel.Email;
            if (ModelState.IsValid)
            {
                if (ViewBag.Firstname != null
                    || ViewBag.Lastname != null
                    || ViewBag.Username != null
                    || ViewBag.Password != null
                    || ViewBag.Email != null)
                {
                    if (userviewmodel.AddEditUser(userviewmodel) == false)
                    {
                        userviewmodel.HelperType = "add";
                        ViewBag.Message = "No Special Characters";
                        return View("~/Views/Home/Register.cshtml");
                    }
                    userviewmodel.HelperType = "add";
                    userviewmodel.AddEditUser(userviewmodel);
                    return View("~/Views/Home/Login.cshtml");
                }
                ViewBag.Message = "All Boxes must be filled";
                return View("~/Views/Home/Register.cshtml");
            }
            return View("~/Views/Home/Register.cshtml");
        }

        [HttpPost]
        [ActionName("login")]
        public IActionResult LoginCheck(UserViewModel userviewmodel)
        {

            if (userviewmodel.LoginValidator(userviewmodel) != null)
                {
                HttpContext.Session.SetString("Username", userviewmodel.Username);

                
                HttpContext.Session.SetInt32("UserId", userviewmodel.UserId);
                return RedirectToAction("profile", "User");
                }
                ViewBag.Message = "Username/Password is incorrect";
                return View("~/Views/Home/Login.cshtml");
         }
        
        [HttpGet]
        [ActionName("profile")]
        public IActionResult AccountPull()
        {
            var sessionusername = HttpContext.Session.GetString("Username");
            var sessionuserid = HttpContext.Session.GetInt32("UserId");
           
            int sessionuserid2;
            sessionuserid2 = sessionuserid.Value;
            

            var userdata = new UserViewModel
            {
                UserId = sessionuserid2,
                Username = sessionusername
                
            };
            return RedirectToAction("Account", "Home", userdata);
        }

        [HttpGet]
        [ActionName("Logout")]
        public IActionResult AccountLogOut()
        {
            HttpContext.Session.Clear();
            return View("~/Views/Home/Login.cshtml");
        }
            

        [HttpPost]
        [ActionName("showusername")]
        public IActionResult PostUserName()
        {
            var sessionusername = HttpContext.Session.GetString("Username");

            var userdata = new UserViewModel();

           string useruser = sessionusername;

            ViewBag.Username = useruser;

            userdata.UserNametag(ViewBag.Username);

            return View();
        }

   
    }
}
