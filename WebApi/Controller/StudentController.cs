using Domain.Dtos.Students;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
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
    public async Task<Response<List<GetStudentDto>>> GetStudents()
    {
        return await service.GetStudents();
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetStudentDto>> GetStudentById(int id)
    {
        return await service.GetStudentById(id);
    }
}
