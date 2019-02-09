using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SneakerDrop.Mvc.Models;

namespace SneakerDrop.Mvc.Controllers
{
    public class UserController : Controller
    {

        [HttpPost]
        [ActionName("register")]
        public IActionResult LoginFilter(UserViewModel userviewmodel)
        {
            
            ViewBag.Firstname = userviewmodel.Firstname;
            ViewBag.Lastname = userviewmodel.Lastname;
            ViewBag.Username = userviewmodel.Username;
            ViewBag.Password = userviewmodel.Password;
            ViewBag.Email = userviewmodel.Email;
        
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
    }
}