using Domain.Dtos;
using Domain.Dtos.Courses;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CourseController(ICourseService service)
{
    [HttpPost]
    public async Task<Response<GetCourseDto>> CreateCourse(CreateCourseDto createCourseDto)
    {
        return await service.CreateCourse(createCourseDto);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetCourseDto>> UpdateCourse(int id, UpdateCourseDto updateCourseDto)
    {
        return await service.UpdateCourse(id, updateCourseDto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteCourse(int id)
    {
        return await service.DeleteCourse(id);
    }
    [HttpGet]
    public async Task<Response<List<GetCourseDto>>> GetCourses([FromQuery] CourseFilter filter)
    {
        return await service.GetCourses(filter);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetCourseDto>> GetCourseById(int id)
    {
        return await service.GetCourseById(id);
    }

    [HttpGet("student-per-course")]
    public async Task<Response<List<StudentCountDto>>> GetCountStudentPerCourse()
    {
        return await service.GetCountStudentPerCourse();
    }

    [HttpGet("average")]
    public async Task<Response<List<StudentCountDto>>> GetAvgStudentPerCourse()
    {
        return await service.GetAvgStudentPerCourse();
    }

    [HttpGet("number-of-courses-each-student")]
    public async Task<Response<List<StudentCountDto>>> Task3()
    {
        return await service.Task3();
    }
}
