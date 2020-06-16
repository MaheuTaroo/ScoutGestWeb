using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScoutGestWeb.Controllers
{
    public class TiposPagsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}