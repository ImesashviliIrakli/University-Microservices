using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using University.Shared.Dtos.TeacherDtos;
using University.Shared.Interfaces.TeacherInterfaces;
using University.Shared.Utility;

namespace University.Teacher.Controllers;

[Authorize(Roles = SD.RoleTeacher)]
public class TeacherController : Controller
{
    private readonly ITeacherInterface _teacherService;
    public TeacherController(ITeacherInterface teacherService)
    {
        _teacherService = teacherService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        var responseDto = await _teacherService.GetTeacherProfile(userId);
        
        if (responseDto.Result == null)
        {
            return View();
        }

        var teacherDto = JsonConvert.DeserializeObject<TeacherDto>(responseDto.Result.ToString());

        return View(teacherDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProfile(TeacherDto teacherDto)
    {
        if (ModelState.IsValid)
        {
            teacherDto.UserId = Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
            await _teacherService.CreateTeacherProfile(teacherDto);
            return RedirectToAction("Index");
        }
        return View("Index", teacherDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfile(TeacherDto teacherDto)
    {
        if (ModelState.IsValid)
        {
            await _teacherService.UpdateTeacherProfile(teacherDto);
            return RedirectToAction("Index");
        }
        return View("Index", teacherDto);
    }
}
