using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Services.TeacherAPI.Models;
using University.Services.TeacherAPI.Models.Dto;
using University.Services.TeacherAPI.Repositories.IRepositories;

namespace University.Services.TeacherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "TEACHER")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        protected ResponseDto _response;

        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
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
                var courses = await _courseRepository.GetAll();

                _response.Result = _mapper.Map<IEnumerable<CourseDto>>(courses);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto>> Get(int id)
        {
            try
            {
                var course = await _courseRepository.GetById(id);

                if (course == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Course not found";
                    return NotFound(_response);
                }

                var result = _mapper.Map<CourseDto>(course);

                _response.Result = result;

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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto>> Post([FromBody] CourseDto courseDto)
        {
            try
            {
                var course = _mapper.Map<Course>(courseDto);
                var newCourse = await _courseRepository.Create(course);

                _response.Result = _mapper.Map<CourseDto>(newCourse);

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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto>> Put([FromBody] CourseDto courseDto)
        {
            try
            {
                var course = _mapper.Map<Course>(courseDto);
                var updatedCourse = await _courseRepository.Update(course);

                _response.Result = _mapper.Map<CourseDto>(updatedCourse);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseDto>> Delete(int id)
        {
            try
            {
                var deletedCourse = await _courseRepository.Delete(id);

                _response.Result = _mapper.Map<CourseDto>(deletedCourse);

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
}
