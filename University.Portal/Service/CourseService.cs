using University.Portal.Models;
using University.Portal.Models.CourseModels;
using University.Portal.Service.IService;
using static University.Portal.Utility.SD;
using University.Portal.Utility;

namespace University.Portal.Service
{
    public class CourseService : ICourseService
    {
        private readonly IBaseService _baseService;
        public CourseService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> GetAllCourses()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.CourseAPIBase + "/api/Course/"
            });
        }

        public async Task<ResponseDto> GetCourseById(int courseId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.CourseAPIBase + $"/api/Course/{courseId}"
            });
        }

        public async Task<ResponseDto> Add(CourseDto courseDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = courseDto,
                Url = SD.CourseAPIBase + "/api/Course/"
            });
        }

        public async Task<ResponseDto> Update(CourseDto courseDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = courseDto,
                Url = SD.CourseAPIBase + "/api/Course/"
            });
        }

        public async Task<ResponseDto> Delete(int courseId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = SD.CourseAPIBase + $"/api/Course/{courseId}"
            });
        }
    }
}
