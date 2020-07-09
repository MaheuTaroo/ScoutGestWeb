using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using ScoutGestWeb.Models;

namespace ScoutGestWeb.Controllers
{
    [RequireHttps]
    public class AnalisesController : Controller
    {
        public async Task<IActionResult> Movimentos(List<MovimentoViewModel> mvm)
        {
            return await Task.Run(() => new ViewAsPdf(mvm));
        }
        /*public async Task<IActionResult> Index(object model)
        {
            return await Task.Run(() => new ViewAsPdf(model));
        }*/
    }
}