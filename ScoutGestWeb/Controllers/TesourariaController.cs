using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScoutGestWeb.Controllers
{
    public class TesourariaController : Controller
    {
        public IActionResult Index()
        {
            if (UserData.UserData.userData.Count == 0) return RedirectToAction("Index", "Login");
            return View();
        }
    }
}