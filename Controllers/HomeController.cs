using Contract_Administrator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Contract_Administrator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Zobraz� domovksou str�nku aplikace.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
