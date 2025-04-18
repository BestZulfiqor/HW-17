using System.Net;
using AutoMapper;
using Domain.Dtos.Enrollments;
using Domain.Entities;
using Domain.Filters;
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

    public async Task<Response<List<GetEnrollmentDto>>> GetEnrollments(EnrollmentFilter filter)
    {
        try
        {
            var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

            var enrollments = context.Enrollments.AsQueryable();

            if (filter.From != null)
            {
                var minut = DateTime.UtcNow.Minute;
                enrollments = enrollments.Where(s => minut - s.EnrollmentDate.Minute >= filter.From);
            }
            
            if (filter.To != null)
            {
                var minut = DateTime.UtcNow.Minute;
                enrollments = enrollments.Where(e => minut - e.EnrollmentDate.Minute <= filter.To);
            }

            var mapped = mapper.Map<List<GetEnrollmentDto>>(enrollments);

            var totalRecords = mapped.Count;

            var data = mapped
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            return new PagedResponce<List<GetEnrollmentDto>>(data, validFilter.PageNumber, validFilter.PageSize,
                totalRecords);
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex); // TODO
            throw;
        }
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
