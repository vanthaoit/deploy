using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LogixHealth.Eligibility.Controllers
{
    public class FileViewerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ViewResult> GridView()
        {
            //ViewData[CrossActionKeys.Tabs] = MenuConstants.FileViewer;
            return View();
        }
    }
}