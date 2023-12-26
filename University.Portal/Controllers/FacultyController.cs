using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using University.Portal.Models;
using University.Portal.Models.CourseModels;
using University.Portal.Service.IService;

namespace University.Portal.Controllers
{
    [Authorize]
    public class FacultyController : Controller
    {
        private readonly IFacultyService _facultyService;
        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        public async Task<IActionResult> Index()
        {
            ResponseDto responseDto = await _facultyService.GetAllFaculties();

            if(responseDto == null || !responseDto.IsSuccess)
            {
                TempData["error"] = "Something went wrong";
                return View(new List<FacultyDto>());
            }

            string resultString = Convert.ToString(responseDto.Result);
            IEnumerable<FacultyDto> faculties = JsonConvert.DeserializeObject<IEnumerable<FacultyDto>>(resultString);

            return View(faculties);
        }

        #region Add new faculty
        public IActionResult AddFaculty()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFaculty(FacultyDto facultyDto)
        {
            if (!ModelState.IsValid)
            {
                return View(facultyDto);
            }

            ResponseDto responseDto = await _facultyService.Add(facultyDto);

            if (responseDto == null || !responseDto.IsSuccess)
            {
                TempData["error"] = "Something went wrong";
                return View(facultyDto);
            }

            TempData["success"] = "Faculty added successfully";
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Update existing faculty

        public async Task<IActionResult> UpdateFaculty(int facultyId)
        {
            ResponseDto responseDto = await _facultyService.GetFacultyById(facultyId);

            string resultString = Convert.ToString(responseDto.Result);

            FacultyDto facultyDto = JsonConvert.DeserializeObject<FacultyDto>(resultString);

            return View(facultyDto);
        }

        #endregion
    }
}
