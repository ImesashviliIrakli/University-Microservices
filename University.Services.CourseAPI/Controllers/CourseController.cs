using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Services.CourseAPI.Migrations;
using University.Services.CourseAPI.Models;
using University.Services.CourseAPI.Models.Dto;
using University.Services.CourseAPI.Repositories;
using University.Services.CourseAPI.Repositories.IRepositories;

namespace University.Services.CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private ResponseDto _response;
        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Course> courses = _courseRepository.GetAll();
                IEnumerable<CourseDto> result = _mapper.Map<IEnumerable<CourseDto>>(courses);

                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet("{courseId:int}")]
        public ResponseDto Get(int courseId)
        {
            try
            {
                if(courseId == 0)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Bad Request";

                    return _response;
                }

                Course course = _courseRepository.GetCourse(courseId);

                if(course == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Could not find course";

                    return _response;
                }

                CourseDto result = _mapper.Map<CourseDto>(course);

                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CourseDto courseDto) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Bad Request";

                    return _response;
                }

                Course addedCourse = _courseRepository.Add(_mapper.Map<Course>(courseDto));

                CourseDto result = _mapper.Map<CourseDto>(addedCourse);

                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CourseDto courseDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Bad Request";

                    return _response;
                }

                Course updatedCourse = _courseRepository.Update(_mapper.Map<Course>(courseDto));

                CourseDto result = _mapper.Map<CourseDto>(updatedCourse);

                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete("{courseId:int}")]
        public ResponseDto Delete(int courseId)
        {
            try
            {
                if(courseId == 0 || !ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Bad Request";

                    return _response;
                }

                Course deletedCourse = _courseRepository.Delete(courseId);

                if(deletedCourse == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Could not find course";

                    return _response;
                }

                CourseDto result = _mapper.Map<CourseDto>(deletedCourse);

                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
