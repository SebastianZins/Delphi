using Microsoft.AspNetCore.Mvc;

namespace Delphi.Controllers
{
    public class Health : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
