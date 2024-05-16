using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using University.Shared.Dtos.CourseDtos;
using University.Shared.Dtos.MainDtos;
using University.Shared.Interfaces.CourseInterfaces;

namespace University.Portal.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    private readonly IFacultyService _facultyService;
    public CourseController(ICourseService courseService, IFacultyService facultyService)
    {
        _courseService = courseService;
        _facultyService = facultyService;
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
        IEnumerable<CourseDto> courses = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(resultString);

        return View(courses);
    }

    public async Task<IActionResult> AddCourse()
    {
        ResponseDto responseDto = await _facultyService.GetAllFaculties();
        string resultString = Convert.ToString(responseDto.Result);
        ViewBag.Faculties = JsonConvert.DeserializeObject<IEnumerable<FacultyDto>>(resultString);

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

        TempData["success"] = "Course added successfully";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateCourse(int courseId)
    {
        ResponseDto responseDto = await _courseService.GetCourseById(courseId);

        string responseString = Convert.ToString(responseDto.Result);

        CourseDto courseDto = JsonConvert.DeserializeObject<CourseDto>(responseString);

        // Get faculties
        ResponseDto faculties = await _facultyService.GetAllFaculties();
        string facultiesString = Convert.ToString(faculties.Result);
        ViewBag.Faculties = JsonConvert.DeserializeObject<IEnumerable<FacultyDto>>(facultiesString);

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

        TempData["success"] = "Course updated successfully";
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
            TempData["success"] = "Course deleted successfully";
        }
        return RedirectToAction(nameof(Index));
    }

}
