using System.Net;
using AutoMapper;
using Domain.Dtos.CourseAssignments;
using Domain.Entities;
using Domain.Filters;
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

    public async Task<Response<List<GetCourseAssignmentDto>>> GetCourseAssignments(CourseAssignmentFilter filter)
    {
        try
        {
            var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

            var courses = context.CourseAssignments.AsQueryable();

            var mapped = mapper.Map<List<GetCourseAssignmentDto>>(courses);

            var totalRecords = mapped.Count;

            var data = mapped
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            return new PagedResponce<List<GetCourseAssignmentDto>>(data, validFilter.PageNumber, validFilter.PageSize, totalRecords);
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex);
            throw;
        }
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
