using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Logixhealth.Elgibility.UI.Models;

namespace LogixHealth.Eligibility.Controllers
{
    public class HomeController : Controller //.LogixController
    {
        public IActionResult Index()
        {
            ViewData[CrossActionKeys.Tabs] = MenuConstants.Home;
            return RedirectToAction("ValidationCodes", "ValidationCode");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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