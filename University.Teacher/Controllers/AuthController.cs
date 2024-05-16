using Microsoft.AspNetCore.Mvc;

namespace University.Teacher.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
