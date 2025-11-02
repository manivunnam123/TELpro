using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TELpro.Models;

namespace TELpro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _con;

        public HomeController(ILogger<HomeController> logger, IConfiguration con)
        {
            _logger = logger;
            _con = con;
        }

        public IActionResult Index()
        {
            ViewBag.T = _con["ConnectionString"];
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
