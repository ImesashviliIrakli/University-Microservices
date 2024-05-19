using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Services.CourseAPI.Models;
using University.Services.CourseAPI.Models.Dto;
using University.Services.CourseAPI.Repositories.IRepositories;

namespace University.Services.CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IMapper _mapper;
        private ResponseDto _response;
        public FacultyController(IFacultyRepository facultyRepository, IMapper mapper)
        {
            _facultyRepository = facultyRepository;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Faculty> faculties = _facultyRepository.GetAll();
                IEnumerable<FacultyDto> result = _mapper.Map<IEnumerable<FacultyDto>>(faculties);

                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet("{facultyId:int}")]
        public ResponseDto Get(int facultyId)
        {
            try
            {
                Faculty faculty = _facultyRepository.GetFaculty(facultyId);

                if(faculty == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Could not find faculty";

                    return _response;
                }

                FacultyDto result = _mapper.Map<FacultyDto>(faculty);

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
        public ResponseDto Post([FromBody] FacultyDto facultyDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Bad request";

                    return _response;
                }

                Faculty newFaculty = _facultyRepository.Add(_mapper.Map<Faculty>(facultyDto));
                FacultyDto result = _mapper.Map<FacultyDto>(newFaculty);

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
        public ResponseDto Put([FromBody] FacultyDto facultyDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 400;

                    _response.IsSuccess = false;
                    _response.Message = "Bad request";

                    return _response;
                }

                Faculty updatedFaculty = _facultyRepository.Update(_mapper.Map<Faculty>(facultyDto));
                FacultyDto result = _mapper.Map<FacultyDto>(updatedFaculty);

                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete("{facultyId:int}")]
        public ResponseDto Delete(int facultyId)
        {
            try
            {
                if(facultyId == 0)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid facultyId";

                    return _response;
                }

                Faculty deletedFaculty = _facultyRepository.Delete(facultyId);

                if(deletedFaculty == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Could not delete";

                    return _response;
                }

                FacultyDto result = _mapper.Map<FacultyDto>(deletedFaculty);

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
