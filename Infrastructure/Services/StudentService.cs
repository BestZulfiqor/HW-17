using System.Net;
using AutoMapper;
using Domain.Dtos.Students;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StudentService(DataContext context, IMapper mapper) : IStudentService
{
    public async Task<Response<GetStudentDto>> CreateStudent(CreateStudentDto createStudentDto)
    {
        var student = mapper.Map<Student>(createStudentDto);
        await context.Students.AddAsync(student);
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetStudentDto>(student);
        return result == 0
            ? new Response<GetStudentDto>(HttpStatusCode.BadRequest, "Student not created")
            : new Response<GetStudentDto>(dto);
    }

    public async Task<Response<string>> DeleteStudent(int id)
    {
        var exist = await context.Students.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Instructor not found");
        }
        context.Students.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Student not deleted")
            : new Response<string>("Student deleted");
    }

    public async Task<Response<GetStudentDto>> GetStudentById(int id)
    {
        var exist = await context.Students.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetStudentDto>(HttpStatusCode.NotFound, "Instructor not found");
        }

        var dto = mapper.Map<GetStudentDto>(exist);

        return new Response<GetStudentDto>(dto);
    }

    public async Task<Response<List<GetStudentDto>>> GetStudents(StudentFilter filter)
    {
        try
        {
            var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

            var query = context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(s => 
                    (s.FirstName + " " + s.LastName).ToLower()
                    .Contains(filter.Name.ToLower()));
            }

            if (filter.From != null)
            {
                var year = DateTime.UtcNow.Year;
                query = query.Where(s => year - s.BirthDate.Year >= filter.From);
            }

            if (filter.To != null)
            {
                var year = DateTime.UtcNow.Year;
                query = query.Where(s => year - s.BirthDate.Year <= filter.To);
            }

            var totalRecords = await query.CountAsync();

            var students = await query
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();

            var mapped = mapper.Map<List<GetStudentDto>>(students);

            return new PagedResponce<List<GetStudentDto>>(
                mapped, 
                validFilter.PageNumber, 
                validFilter.PageSize,
                totalRecords);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Response<List<GetStudentDto>>> GetStudentsWithoutCourse()
    {
        var students = await context.Students.Where(n => n.Enrollments.Count == 0)
        .ToListAsync();

        var data = mapper.Map<List<GetStudentDto>>(students);
        return new Response<List<GetStudentDto>>(data);
    }


    public async Task<Response<GetStudentDto>> UpdateStudent(int id, UpdateStudentDto updateStudentDto)
    {
        var exist = await context.Students.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetStudentDto>(HttpStatusCode.NotFound, "Instructor not found");
        }

        exist.BirthDate = updateStudentDto.BirthDate;
        exist.FirstName = updateStudentDto.FirstName;
        exist.LastName = updateStudentDto.LastName;
        var result = await context.SaveChangesAsync();

        var dto = mapper.Map<GetStudentDto>(exist);

        return result == 0
            ? new Response<GetStudentDto>(HttpStatusCode.BadRequest, "Student not updated")
            : new Response<GetStudentDto>(dto);
    }
}
