using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using University.Portal.Models;
using University.Portal.Models.CourseModels;
using University.Portal.Service;
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

        public async Task<IActionResult> Index()
        {
            ResponseDto responseDto = await _courseService.GetAllCourses();

            if (responseDto == null || !responseDto.IsSuccess)
            {
                TempData["error"] = "Something went wrong";
                return View(new List<CourseDto>());
            }

            string resultString = Convert.ToString(responseDto.Result);
            IEnumerable<FacultyDto> faculties = JsonConvert.DeserializeObject<IEnumerable<FacultyDto>>(resultString);

            return View(faculties);
        }

        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return View(courseDto);
            }

            ResponseDto responseDto = await _courseService.Add(courseDto);

            if (responseDto == null || !responseDto.IsSuccess)
            {
                TempData["error"] = "Something went wrong";
                return View(courseDto);
            }

            TempData["success"] = "Faculty added successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateCourse(int courseId)
        {
            ResponseDto responseDto = await _courseService.GetCourseById(courseId);

            string responseString = Convert.ToString(responseDto.Result);

            CourseDto courseDto = JsonConvert.DeserializeObject<CourseDto>(responseString);

            return View(courseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return View(courseDto);
            }

            ResponseDto responseDto = await _courseService.Update(courseDto);

            if (responseDto == null || !responseDto.IsSuccess)
            {
                TempData["error"] = "Something went wrong";
                return View(courseDto);
            }

            TempData["success"] = "Faculty added successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            ResponseDto responseDto = await _courseService.Delete(courseId);

            if (responseDto == null || !responseDto.IsSuccess)
            {
                TempData["error"] = "Something went wrong";
            }
            else
            {
                TempData["success"] = "Faculty deleted successfully";
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
