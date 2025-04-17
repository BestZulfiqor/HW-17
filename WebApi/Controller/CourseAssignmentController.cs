using Domain.Dtos.CourseAssignments;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
public class CourseAssignmentController(ICourseAssignmentService service)
{
    [HttpPost]
    public async Task<Response<GetCourseAssignmentDto>> CreateCourseAssignment(CreateCourseAssignmentDto createCourseAssignmentDto){
        return await service.CreateCourseAssignment(createCourseAssignmentDto);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetCourseAssignmentDto>> UpdateCourseAssignment(int id, UpdateCourseAssignmentDto updateCourseAssignmentDto){
        return await service.UpdateCourseAssignment(id, updateCourseAssignmentDto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteCourseAssignment(int id){
        return await service.DeleteCourseAssignment(id);
    }
    [HttpGet]
    public async Task<Response<List<GetCourseAssignmentDto>>> GetCourseAssignments(){
        return await service.GetCourseAssignments();
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetCourseAssignmentDto>> GetCourseAssignmentById(int id){
        return await service.GetCourseAssignmentById(id);
    }
}
