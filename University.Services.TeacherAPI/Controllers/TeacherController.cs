using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Services.TeacherAPI.Models;
using University.Services.TeacherAPI.Models.Dto;
using University.Services.TeacherAPI.Repositories.IRepositories;

namespace University.Services.TeacherAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "TEACHER")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    protected ResponseDto _response;

    public TeacherController(ITeacherRepository teacherRepository, ICourseRepository courseRepository, IMapper mapper)
    {
        _teacherRepository = teacherRepository;
        _courseRepository = courseRepository;
        _mapper = mapper;
        _response = new();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseDto>> Get()
    {
        try
        {
            var teachers = await _teacherRepository.GetAll();

            _response.Result = _mapper.Map<IEnumerable<TeacherDto>>(teachers);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;

            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseDto>> Get(Guid id)
    {
        try
        {
            var teacher = await _teacherRepository.GetById(id);

            if (teacher == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Teacher not found";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            _response.Result = _mapper.Map<TeacherDto>(teacher);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpGet("GetByUserId/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseDto>> GetByUserId(Guid userId)
    {
        try
        {
            var teacher = await _teacherRepository.GetByUserId(userId);

            if (teacher == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Profile not found, You must create profile";
                return StatusCode(StatusCodes.Status404NotFound, _response);
            }

            _response.Result = _mapper.Map<TeacherDto>(teacher);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseDto>> Post([FromBody] TeacherDto teacherDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.Message = ModelState.ToString();

                return StatusCode(StatusCodes.Status400BadRequest, _response);
            }

            var teacher = _mapper.Map<Teacher>(teacherDto);

            var createdTeacher = await _teacherRepository.Create(teacher);

            if (createdTeacher == null)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record");

                _response.IsSuccess = false;
                _response.Message = ModelState.ToString();

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            _response.Result = _mapper.Map<TeacherDto>(createdTeacher);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseDto>> Put([FromBody] TeacherDto teacherDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.Message = ModelState.ToString();

                return StatusCode(StatusCodes.Status400BadRequest, _response);
            }

            var teacher = _mapper.Map<Teacher>(teacherDto);

            var updatedTeacher = await _teacherRepository.Update(teacher);

            if (updatedTeacher == null)
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record");

                _response.IsSuccess = false;
                _response.Message = ModelState.ToString();

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            _response.Result = _mapper.Map<TeacherDto>(updatedTeacher);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseDto>> Delete(Guid id)
    {
        try
        {
            var deleteTeacher = await _teacherRepository.Delete(id);

            if (!deleteTeacher)
            {
                _response.IsSuccess = false;
                _response.Message = "Could not delete";

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }
}
