using Domain.Dtos;
using Domain.Dtos.Instructors;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InstructorController(IInstructureService service)
{
    [HttpPost]
    public async Task<Response<GetInstructorDto>> CreateInstructor(CreateInstructorDto createInstructorDto)
    {
        return await service.CreateInstructor(createInstructorDto);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetInstructorDto>> UpdateInstructor(int id, UpdateInstructorDto updateInstructorDto)
    {
        return await service.UpdateInstructor(id, updateInstructorDto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteInstructor(int id)
    {
        return await service.DeleteInstructor(id);
    }
    [HttpGet]
    public async Task<Response<List<GetInstructorDto>>> GetInstructors([FromQuery]InstructorFilter filter)
    {
        return await service.GetInstructors(filter);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetInstructorDto>> GetInstructorById(int id)
    {
        return await service.GetInstructorById(id);
    }

    [HttpGet("instructors-with-course-count")]
    public async Task<Response<List<StudentCountDto>>> GetInstructorsWithCourseCount(){
        return await service.GetInstructorsWithCourseCount();
    }
}
