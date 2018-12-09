using Microsoft.AspNetCore.Mvc;

namespace Ajj.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}