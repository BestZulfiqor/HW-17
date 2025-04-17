using System.Net;
using AutoMapper;
using Domain.Dtos.CourseAssignments;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseAssignmentService(DataContext context, IMapper mapper) : ICourseAssignmentService
{
    public async Task<Response<GetCourseAssignmentDto>> CreateCourseAssignment(CreateCourseAssignmentDto createCourseAssignmentDto)
    {
        var assignment = mapper.Map<CourseAssignment>(createCourseAssignmentDto);
        await context.CourseAssignments.AddAsync(assignment);
        var result = await context.SaveChangesAsync();

        var dto = mapper.Map<GetCourseAssignmentDto>(assignment);

        return result == 0
            ? new Response<GetCourseAssignmentDto>(HttpStatusCode.BadRequest, "Course assignment not created")
            : new Response<GetCourseAssignmentDto>(dto);
    }

    public async Task<Response<string>> DeleteCourseAssignment(int id)
    {
        var exist = await context.CourseAssignments.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Course assignment not found");
        }
        context.CourseAssignments.Remove(exist);
        var result = await context.SaveChangesAsync();

        return result == 0
                    ? new Response<string>(HttpStatusCode.BadRequest, "Course assignment not deleted")
                    : new Response<string>("Course assignment deleted");
    }

    public async Task<Response<GetCourseAssignmentDto>> GetCourseAssignmentById(int id)
    {
        var exist = await context.CourseAssignments.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCourseAssignmentDto>(HttpStatusCode.NotFound, "Course assignment not found");
        }
        var dto = mapper.Map<GetCourseAssignmentDto>(exist);
        return new Response<GetCourseAssignmentDto>(dto);
    }

    public async Task<Response<List<GetCourseAssignmentDto>>> GetCourseAssignments()
    {
        var courses = await context.CourseAssignments.ToListAsync();
        var data = mapper.Map<List<GetCourseAssignmentDto>>(courses);
        return new Response<List<GetCourseAssignmentDto>>(data);
    }

    public async Task<Response<GetCourseAssignmentDto>> UpdateCourseAssignment(int id, UpdateCourseAssignmentDto updateCourseAssignmentDto)
    {
        var exist = await context.CourseAssignments.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCourseAssignmentDto>(HttpStatusCode.NotFound, "Course assignment not found");
        }
        exist.CourseId = updateCourseAssignmentDto.CourseId;
        exist.InstructorId = updateCourseAssignmentDto.InstructorId;
        
        var dto = mapper.Map<GetCourseAssignmentDto>(exist);

        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<GetCourseAssignmentDto>(HttpStatusCode.BadRequest, "Course assignment not updated")
            : new Response<GetCourseAssignmentDto>(dto);
    }
}
