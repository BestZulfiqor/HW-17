using Domain.Dtos;
using Domain.Dtos.Instructors;
using Domain.Filters;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IInstructureService
{
    Task<Response<GetInstructorDto>> CreateInstructor(CreateInstructorDto createInstructorDto);
    Task<Response<GetInstructorDto>> UpdateInstructor(int id, UpdateInstructorDto updateInstructorDto);
    Task<Response<string>> DeleteInstructor(int id);
    Task<Response<List<GetInstructorDto>>> GetInstructors(InstructorFilter filter);
    Task<Response<GetInstructorDto>> GetInstructorById(int id);
    Task<Response<List<StudentCountDto>>> GetInstructorsWithCourseCount();
}
