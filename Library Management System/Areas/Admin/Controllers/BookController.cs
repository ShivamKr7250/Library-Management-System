using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
