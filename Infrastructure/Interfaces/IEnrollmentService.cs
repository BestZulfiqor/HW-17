using Domain.Dtos.Enrollments;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IEnrollmentService
{
    Task<Response<GetEnrollmentDto>> CreateEnrollment(CreateEnrollmentDto createEnrollmentDto);
    Task<Response<GetEnrollmentDto>> UpdateEnrollment(int id, UpdateEnrollmentDto updateEnrollmentDto);
    Task<Response<string>> DeleteEnrollment(int id);
    Task<Response<List<GetEnrollmentDto>>> GetEnrollments();
    Task<Response<GetEnrollmentDto>> GetEnrollmentById(int id);
}
