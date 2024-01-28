using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Services.StudentAPI.Models;
using University.Services.StudentAPI.Models.Dto;
using University.Services.StudentAPI.Repositories.IRepositories;

namespace University.Services.StudentAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentController : ControllerBase
{
	private readonly IStudentRepository _studentRepository;
	private readonly IMapper _mapper;
	protected ResponseDto _response;
	public StudentController(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _response = new();
        _mapper = mapper;
    }

    [HttpGet]
	public async Task<IActionResult> GetStudents()
	{
        try
        {
            var students = await _studentRepository.GetStudents();
            var result = _mapper.Map<IEnumerable<StudentDto>>(students);

            _response.Result = result;

            return Ok(_response);
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;

            return StatusCode(500, _response);
        }
    }

    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetStudentById(Guid studentId)
    {
        try
        {
            var student = await _studentRepository.GetStudentById(studentId);

            if (student == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Student not found";
                return NotFound(_response);
            }

            var result = _mapper.Map<StudentDto>(student);

            _response.Result = result;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;

            return StatusCode(500, _response);
        }
    }

    [HttpGet("GetStudentByEmail/{email}")]
    public async Task<IActionResult> GetStudentByEmail(string email)
    {
        try
        {
            var student = await _studentRepository.GetStudentByEmail(email);

            if (student == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Student not found";
                return NotFound(_response);
            }

            var result = _mapper.Map<StudentDto>(student);

            _response.Result = result;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;

            return StatusCode(500, _response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] StudentDto studentDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.Message = "Student model is not valid";
                return BadRequest(_response);
            }

            var student = _mapper.Map<Student>(studentDto);
            var newStudent = await _studentRepository.AddStudent(student);

            if (newStudent == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Student creation failed";
                return StatusCode(500, _response);
            }

            _response.Result = student;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;

            return StatusCode(500, _response);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] StudentDto studentDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.Message = "Student model is not valid";
                return BadRequest(_response);
            }

            var student = _mapper.Map<Student>(studentDto);
            var updatedStudent = await _studentRepository.UpdateStudent(student);

            if (updatedStudent == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Student update failed";
                return StatusCode(500, _response);
            }

            _response.Result = student;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;

            return StatusCode(500, _response);
        }
    }

    [HttpDelete("{studentId}")]
    public async Task<IActionResult> Delete(Guid studentId)
    {
        try
        {
            var result = await _studentRepository.DeleteStudent(studentId);

            if (!result)
            {
                _response.IsSuccess = false;
                _response.Message = "Student deletion failed";
                return StatusCode(500, _response);
            }

            _response.Message = "Student deleted successfully";

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;

            return StatusCode(500, _response);
        }
    }
}
