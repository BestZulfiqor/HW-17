using Domain.Dtos.Students;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentController(IStudentService service)
{
    [HttpPost]
    public async Task<Response<GetStudentDto>> CreateStudent(CreateStudentDto createStudentDto)
    {
        return await service.CreateStudent(createStudentDto);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetStudentDto>> UpdateStudent(int id, UpdateStudentDto updateStudentDto)
    {
        return await service.UpdateStudent(id, updateStudentDto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteStudent(int id)
    {
        return await service.DeleteStudent(id);
    }
    [HttpGet]
    public async Task<Response<List<GetStudentDto>>> GetStudents([FromQuery] StudentFilter filter)
    {
        return await service.GetStudents(filter);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetStudentDto>> GetStudentById(int id)
    {
        return await service.GetStudentById(id);
    }

    [HttpGet("student-with-course-count")]
    public async Task<Response<List<GetStudentDto>>> GetStudentsWithoutCourse()
    {
        return await service.GetStudentsWithoutCourse();
    }
}
