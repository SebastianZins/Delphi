using Microsoft.AspNetCore.Mvc;

namespace Delphi.Controllers
{
    public class Search : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
