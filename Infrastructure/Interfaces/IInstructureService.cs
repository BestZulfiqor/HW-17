using Domain.Dtos.Instructors;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IInstructureService
{
    Task<Response<GetInstructorDto>> CreateInstructor(CreateInstructorDto createInstructorDto);
    Task<Response<GetInstructorDto>> UpdateInstructor(int id, UpdateInstructorDto updateInstructorDto);
    Task<Response<string>> DeleteInstructor(int id);
    Task<Response<List<GetInstructorDto>>> GetInstructors();
    Task<Response<GetInstructorDto>> GetInstructorById(int id);
}
