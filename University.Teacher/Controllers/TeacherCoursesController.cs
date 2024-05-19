using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using University.Shared.Dtos.CourseDtos;
using University.Shared.Dtos.TeacherDtos;
using University.Shared.Interfaces.CourseInterfaces;
using University.Shared.Interfaces.TeacherInterfaces;

namespace University.Teacher.Controllers;

[Authorize(Roles = "TEACHER")]
public class TeacherCoursesController : Controller
{
    private readonly ITeacherCoursesInterface _teacherCoursesService;
    private readonly ICourseService _courseService;
    private readonly IFacultyService _facultyService;

    public TeacherCoursesController(
        ITeacherCoursesInterface teacherCoursesService,
        ICourseService courseService,
        IFacultyService facultyService
        )
    {
        _teacherCoursesService = teacherCoursesService;
        _courseService = courseService;
        _facultyService = facultyService;
    }

    public async Task<IActionResult> Index()
    {
        var responseDto = await _teacherCoursesService.GetTeacherCourses();

        var courses = new List<TeacherCoursesDto>();

        if (responseDto.Result != null)
            courses = JsonConvert.DeserializeObject<List<TeacherCoursesDto>>(responseDto.Result.ToString());

        return View(courses);
    }

    [HttpGet]
    public async Task<JsonResult> GetCourses()
    {
        var responseDto = await _courseService.GetAllCourses();
        var courses = JsonConvert.DeserializeObject<List<CourseDto>>(responseDto.Result.ToString());
        return Json(courses);
    }

    [HttpGet]
    public async Task<JsonResult> GetFaculties()
    {
        var responseDto = await _facultyService.GetAllFaculties();

        var faculties = new List<FacultyDto>();

        if(responseDto.Result != null)
            faculties = JsonConvert.DeserializeObject<List<FacultyDto>>(responseDto.Result.ToString());
        
        return Json(faculties);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCourse(TeacherCoursesDto teacherCoursesDto)
    {
        if (ModelState.IsValid)
        {
            teacherCoursesDto.UserId = Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
            var response = await _teacherCoursesService.AddTeacherCourses(teacherCoursesDto);
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, response.Message);
        }
        return View("Index", new List<TeacherCoursesDto>());
    }
}
