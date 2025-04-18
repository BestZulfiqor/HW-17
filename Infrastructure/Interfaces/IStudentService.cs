using Domain.Dtos.Students;
using Domain.Filters;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IStudentService
{
    Task<Response<GetStudentDto>> CreateStudent(CreateStudentDto createStudentDto);
    Task<Response<GetStudentDto>> UpdateStudent(int id, UpdateStudentDto updateStudentDto);
    Task<Response<string>> DeleteStudent(int id);
    Task<Response<List<GetStudentDto>>> GetStudents(StudentFilter filter);
    Task<Response<GetStudentDto>> GetStudentById(int id);
}
