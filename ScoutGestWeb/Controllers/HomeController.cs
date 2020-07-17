using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoutGestWeb.Models;
using System.Threading.Tasks;

namespace ScoutGestWeb.Controllers
{
    [RequireHttps]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _accessor;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _accessor = accessor;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => !User.Identity.IsAuthenticated ? View("Login") : View("Dashboard")); ;
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(login.Username);
                if (user != null)
                {
                    if (user.LockoutEnabled)
                    {
                        ModelState.AddModelError("", "Não é possível iniciar sessão, pois esta conta está trancada");
                        return await Task.Run(() => Index());
                    }
                    var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);
                    if (result.Succeeded) return await Task.Run(() => View("Dashboard"));
                }
                ModelState.AddModelError("", "Credenciais incorretas");
                return await Task.Run(() => Index());
            }
            return await Task.Run(() => View("Login", login));
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return await Task.Run(() => RedirectToAction("Index"));
        }
        public async Task<IActionResult> InfoLogout()
        {
            return await Task.Run(() => !User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : (IActionResult)View());
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            ViewBag.error = statusCode;
            return View();
        }
    }
}