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



namespace SneakerDrop.Mvc.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        [ActionName("register")]
        public IActionResult RegisterFilter(UserViewModel userviewmodel)
        {
          
            if (ModelState.IsValid)
            {
                    userviewmodel.HelperType = "add";
                    if (userviewmodel.AddEditUser(userviewmodel) == false || userviewmodel.EmailValidator(userviewmodel) == false)
                    {
                        ViewBag.Message = "No Special Characters";
                        return View("~/Views/Home/Register.cshtml");
                    }
                    userviewmodel.AddEditUser(userviewmodel);
                return RedirectToAction("Login", "Home");
                }
                ViewBag.Message = "All Boxes must be filled";
                return View("~/Views/Home/Register.cshtml");
            }
            
         [HttpPost]
        [ActionName("login")]
        public IActionResult LoginCheck(UserViewModel userviewmodel)
        {
            var user = userviewmodel.LoginValidator(userviewmodel);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Username", userviewmodel.Username);
                return RedirectToAction("profile", "User");
            }
            ViewBag.Message = "Username and/or Password is incorrect";

            return View("~/Views/Home/Login.cshtml");
        }

        [HttpGet]
        [ActionName("profile")]
        public IActionResult AccountPull()
        {
            var sessionuserid = HttpContext.Session.GetInt32("UserId");
            var sessionusername = HttpContext.Session.GetString("Username");

            var userdata = new UserViewModel
            {
                UserId = (int)sessionuserid,
                Username = sessionusername

            };
            return RedirectToAction("Account", "Home", userdata);
        }

        [HttpGet]
        [ActionName("Logout")]
        public IActionResult AccountLogOut()
        {
            HttpContext.Session.Clear();
           return RedirectToAction("Login", "Home");
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
