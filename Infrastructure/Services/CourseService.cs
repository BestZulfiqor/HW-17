using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.Courses;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseService(DataContext context, IMapper mapper) : ICourseService
{
    public async Task<Response<GetCourseDto>> CreateCourse(CreateCourseDto createCourseDto)
    {
        var course = mapper.Map<Course>(createCourseDto);
        await context.Courses.AddAsync(course);
        var result = await context.SaveChangesAsync();

        var dto = mapper.Map<GetCourseDto>(course);

        return result == 0
            ? new Response<GetCourseDto>(HttpStatusCode.BadRequest, "Course not created")
            : new Response<GetCourseDto>(dto);
    }

    public async Task<Response<string>> DeleteCourse(int id)
    {
        var exist = await context.Courses.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Course not found");
        }
        context.Courses.Remove(exist);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Course not deleted")
            : new Response<string>("Course deleted");
    }

    public async Task<Response<List<StudentCountDto>>> GetAvgStudentPerCourse()
    {
        var students = await context.Courses.Select(n => new StudentCountDto
        {
            Title = n.Title,
            Count = n.Enrollments.Average(n => n.Grade)
        }).ToListAsync();

        return new Response<List<StudentCountDto>>(students);
    }


    public async Task<Response<List<StudentCountDto>>> GetCountStudentPerCourse()
    {
        var students = await context.Courses.Select(n => new StudentCountDto
        {
            Title = n.Title,
            Count = n.Enrollments.Count
        }).ToListAsync();

        return new Response<List<StudentCountDto>>(students);
    }


    public async Task<Response<GetCourseDto>> GetCourseById(int id)
    {
        var exist = await context.Courses.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCourseDto>(HttpStatusCode.BadRequest, "Course not found");
        }

        var dto = mapper.Map<GetCourseDto>(exist);

        return new Response<GetCourseDto>(dto);
    }

    public async Task<Response<List<GetCourseDto>>> GetCourses(CourseFilter filter)
    {
        try
        {
            var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);

            var courses = context.Courses.AsQueryable();


            if (filter.Name != null)
            {
                courses = courses.Where(c => c.Title.ToLower().Contains(filter.Name));
            }

            if (filter.From != null)
            {
                courses = courses.Where(c => c.Price <= filter.From);
            }

            if (filter.To != null)
            {
                courses = courses.Where(c => c.Price >= filter.To);
            }

            var mapped = mapper.Map<List<GetCourseDto>>(courses);
            
            var totalRecords = mapped.Count;

            var data = mapped
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            return new PagedResponce<List<GetCourseDto>>(data, validFilter.PageNumber, validFilter.PageSize, totalRecords);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Response<List<StudentCountDto>>> Task3()
    {
        var students = await context.Enrollments.GroupBy(n => n.Course).Select(n => new StudentCountDto{
            Title = n.Key.Title,
            Count = n.Count()
        }).ToListAsync();

        return new Response<List<StudentCountDto>>(students);
    }


    public async Task<Response<GetCourseDto>> UpdateCourse(int id, UpdateCourseDto updateCourseDto)
    {
        var exist = await context.Courses.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetCourseDto>(HttpStatusCode.NotFound, "Course not found");
        }
        exist.Description = updateCourseDto.Description;
        exist.Price = updateCourseDto.Price;
        exist.Title = updateCourseDto.Title;
        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetCourseDto>(exist);

        return result == 0
            ? new Response<GetCourseDto>(HttpStatusCode.BadRequest, "Course not updated")
            : new Response<GetCourseDto>(dto);
    }

}
