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
        [ActionName("login")]
        public IActionResult LoginCheck(UserViewModel userviewmodel)
        {
            ViewBag.Username = userviewmodel.Username;
            ViewBag.Password = userviewmodel.Password;

            if (ViewBag.Username == "OaksTree" && ViewBag.Password == "1234")
            {
                return View("~/Views/User/Account.cshtml");
            }
            return View("Index");
        }
    }
}