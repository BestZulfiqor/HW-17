using Domain.Dtos;
using Domain.Dtos.Courses;
using Domain.Filters;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface ICourseService
{
    Task<Response<GetCourseDto>> CreateCourse(CreateCourseDto createCourseDto);
    Task<Response<GetCourseDto>> UpdateCourse(int id, UpdateCourseDto updateCourseDto);
    Task<Response<string>> DeleteCourse(int id);
    Task<Response<List<GetCourseDto>>> GetCourses(CourseFilter filter);
    Task<Response<GetCourseDto>> GetCourseById(int id);
    Task<Response<List<StudentCountDto>>> GetCountStudentPerCourse();
    Task<Response<List<StudentCountDto>>> GetAvgStudentPerCourse();
    Task<Response<List<StudentCountDto>>> Task3();
}
