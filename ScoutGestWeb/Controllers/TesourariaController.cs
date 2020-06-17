using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScoutGestWeb.Controllers
{
    public class TesourariaController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (UserData.UserData.userData.Count == 0/* || Request.Cookies["User"] == null*/) return await Task.Run(() => RedirectToAction("Index", "Home"));
            return await Task.Run(() => View());
        }
    }
}