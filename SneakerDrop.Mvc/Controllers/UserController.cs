using System;
using System.Collections.Generic;
using System.Linq;
using dm = SneakerDrop.Domain.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SneakerDrop.Mvc.Models;
using SneakerDrop.Code.Helpers;


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
            ViewBag.Username = userviewmodel.Username;
            ViewBag.Password = userviewmodel.Password;

            var domainuser = new dm.User();

           if (userviewmodel.LoginValidator(userviewmodel) != null)
            {
                return View("~/Views/User/Account.cshtml");
            }
            return View("~/Views/Home/Index.cshtml");

         }

        //[HttpPost]
        //public ActionResult Login(UserViewModel userviewmodel)
        //{
          
            
            
            
            
            
            //if (ModelState.IsValid)
            //{
            //    var userdata = new UserViewModel
            //    {
            //        UserId = userviewmodel.UserId,
            //        Username = userviewmodel.Username,
            //        HelperType = userviewmodel.HelperType,
            //        Firstname = userviewmodel.Firstname,
            //        Lastname = userviewmodel.Lastname,
            //        Email = userviewmodel.Email
            //    };
            //    this.S
            //}
        }
    }
