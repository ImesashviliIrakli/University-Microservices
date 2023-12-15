using Microsoft.AspNetCore.Mvc;
using University.Portal.Service.IService;

namespace University.Portal.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
