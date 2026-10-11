using Microsoft.AspNetCore.Mvc;

namespace Delphi.Controllers
{
    public class Vault : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
