using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}