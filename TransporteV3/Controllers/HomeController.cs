using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TransporteV3.Entidades;
using TransporteV3.Models;
using System.Linq;

namespace TransporteV3.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult ReportChoferes()
        //{
        //    return View();
        //}


        public IActionResult ReportUnidades()
        {
            return View();
        }
        public IActionResult ReportViajes()
        {
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