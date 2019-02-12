﻿using System;
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
                    userviewmodel.HelperType = "add";
                    userviewmodel.AddEditUser(userviewmodel);
                    return View("~/Views/User/Account.cshtml");
                }
             
                return View("~/Views/Home/Index.cshtml");
            }
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        [ActionName("login")]
        public IActionResult LoginCheck(UserViewModel userviewmodel)
        {
            foreach (var item in UserHelper.GetAllUsers())
            {
                if (item.Username.ToString() != userviewmodel.Username)
                {
                    ViewBag.Message = "Username/Password is incorrect";
                    return View("~/Views/Home/Login.cshtml");
                }

                if (userviewmodel.LoginValidator(userviewmodel) != null)
                {
                    HttpContext.Session.SetInt32("UserId", userviewmodel.UserId);
                    HttpContext.Session.SetString("Username", userviewmodel.Username);
                    return RedirectToAction("profile", "User");
                }
                return View("~/Views/Home/Register.cshtml");

            }
            return View("~/Views/Home/Register.cshtml");
        }

        [HttpGet]
        [ActionName("profile")]
        public IActionResult AccountPull()
        {
            var sessionuserid = HttpContext.Session.GetInt32("UserId");
            var sessionusername = HttpContext.Session.GetString("Username");
            int sessionuserid2;
            sessionuserid2 = sessionuserid.Value;
            

            var userdata = new UserViewModel
            {
                UserId = sessionuserid2,
                Username = sessionusername
                
            };
            return RedirectToAction("Account", "Home", userdata);
        }

        [HttpPost]
        [ActionName("Logout")]
        public IActionResult AccountLogOut(string AccountLogOut)
        {
            if (AccountLogOut == "LogOut")
            {
                HttpContext.Session.Clear();
                return View("~/Views/Home/Login.cshtml");
        }
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
