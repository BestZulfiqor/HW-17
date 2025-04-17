using Domain.Dtos.Instructors;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
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
    public async Task<Response<List<GetInstructorDto>>> GetInstructors()
    {
        return await service.GetInstructors();
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetInstructorDto>> GetInstructorById(int id)
    {
        return await service.GetInstructorById(id);
    }
}
