using Domain.Dtos.Enrollments;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
public class EnrollmentController(IEnrollmentService service)
{
    [HttpPost]
    public async Task<Response<GetEnrollmentDto>> CreateEnrollment(CreateEnrollmentDto createEnrollmentDto)
    {
        return await service.CreateEnrollment(createEnrollmentDto);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetEnrollmentDto>> UpdateEnrollment(int id, UpdateEnrollmentDto updateEnrollmentDto)
    {
        return await service.UpdateEnrollment(id, updateEnrollmentDto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteEnrollment(int id)
    {
        return await service.DeleteEnrollment(id);
    }
    [HttpGet]
    public async Task<Response<List<GetEnrollmentDto>>> GetEnrollments([FromQuery]EnrollmentFilter filter)
    {
        return await service.GetEnrollments(filter);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetEnrollmentDto>> GetEnrollmentById(int id)
    {
        return await service.GetEnrollmentById(id);
    }
}
