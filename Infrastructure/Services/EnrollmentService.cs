using System.Net;
using AutoMapper;
using Domain.Dtos.Enrollments;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EnrollmentService(DataContext context, IMapper mapper) : IEnrollmentService
{
    public async Task<Response<GetEnrollmentDto>> CreateEnrollment(CreateEnrollmentDto createEnrollmentDto)
    {
        var enrollment = mapper.Map<Enrollment>(createEnrollmentDto);
        await context.Enrollments.AddAsync(enrollment);
        var dto = mapper.Map<GetEnrollmentDto>(enrollment);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<GetEnrollmentDto>(HttpStatusCode.BadRequest, "Enrollment not created")
            : new Response<GetEnrollmentDto>(dto);
    }

    public async Task<Response<string>> DeleteEnrollment(int id)
    {
        var exist = await context.Enrollments.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Enrollment not found");
        }
        context.Enrollments.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Enrollment not deleted")
            : new Response<string>("Enrollment deleted");
    }

    public async Task<Response<GetEnrollmentDto>> GetEnrollmentById(int id)
    {
        var exist = await context.Enrollments.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetEnrollmentDto>(HttpStatusCode.NotFound, "Enrollment not found");
        }

        var dto = mapper.Map<GetEnrollmentDto>(exist);

        return new Response<GetEnrollmentDto>(dto);
    }

    public async Task<Response<List<GetEnrollmentDto>>> GetEnrollments()
    {
        var enrollments = await context.Enrollments.ToListAsync();
        var data = mapper.Map<List<GetEnrollmentDto>>(enrollments);
        return new Response<List<GetEnrollmentDto>>(data);
    }

    public async Task<Response<GetEnrollmentDto>> UpdateEnrollment(int id, UpdateEnrollmentDto updateEnrollmentDto)
    {
        var exist = await context.Enrollments.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetEnrollmentDto>(HttpStatusCode.NotFound, "Enrollment not found");
        }

        exist.CourseId = updateEnrollmentDto.CourseId;
        exist.EnrollmentDate = updateEnrollmentDto.EnrollmentDate;
        exist.Grade = updateEnrollmentDto.Grade;
        exist.StudentId = updateEnrollmentDto.StudentId;

        var result = await context.SaveChangesAsync();
        
        var dto = mapper.Map<GetEnrollmentDto>(exist);

        return result == 0
            ? new Response<GetEnrollmentDto>(HttpStatusCode.BadRequest, "Enrollment not updated")
            : new Response<GetEnrollmentDto>(dto);
    }

}
