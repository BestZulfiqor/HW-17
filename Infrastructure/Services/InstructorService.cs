using System.Net;
using AutoMapper;
using Domain.Dtos.Instructors;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class InstructorService(DataContext context, IMapper mapper) : IInstructureService
{
    public async Task<Response<GetInstructorDto>> CreateInstructor(CreateInstructorDto createInstructorDto)
    {
        var instructor = mapper.Map<Instructor>(createInstructorDto);
        await context.Instructors.AddAsync(instructor);

        var result = await context.SaveChangesAsync();
        var dto = mapper.Map<GetInstructorDto>(instructor);

        return result == 0
            ? new Response<GetInstructorDto>(HttpStatusCode.BadRequest, "Instructor not created")
            : new Response<GetInstructorDto>(dto);
    }

    public async Task<Response<string>> DeleteInstructor(int id)
    {
        var exist = await context.Instructors.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Instructor not found");
        }

        context.Instructors.Remove(exist);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Instructor not deleted")
            : new Response<string>("Instructor deleted");
    }

    public async Task<Response<GetInstructorDto>> GetInstructorById(int id)
    {
        var exist = await context.Instructors.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetInstructorDto>(HttpStatusCode.NotFound, "Instructor not found");
        }

        var dto = mapper.Map<GetInstructorDto>(exist);

        return new Response<GetInstructorDto>(dto);
    }

    public async Task<Response<List<GetInstructorDto>>> GetInstructors()
    {
        var instructors = await context.Instructors.ToListAsync();
        var data = mapper.Map<List<GetInstructorDto>>(instructors);
        return new Response<List<GetInstructorDto>>(data);
    }

    public async Task<Response<GetInstructorDto>> UpdateInstructor(int id, UpdateInstructorDto updateInstructorDto)
    {
        var exist = await context.Instructors.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetInstructorDto>(HttpStatusCode.NotFound, "Instructor not found");
        }
        exist.FirstName = updateInstructorDto.FirstName;
        exist.LastName = updateInstructorDto.LastName;
        exist.Phone = updateInstructorDto.Phone;
        var result = await context.SaveChangesAsync();

        var dto = mapper.Map<GetInstructorDto>(exist);
        return result == 0
            ? new Response<GetInstructorDto>(HttpStatusCode.BadRequest, "Instructor not updated")
            : new Response<GetInstructorDto>(dto);
    }

}
