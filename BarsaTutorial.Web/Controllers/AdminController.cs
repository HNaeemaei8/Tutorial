using Microsoft.AspNetCore.Mvc;

namespace BarsaTutorial.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
