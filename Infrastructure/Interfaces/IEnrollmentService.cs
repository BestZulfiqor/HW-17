using Domain.Dtos.Enrollments;
using Domain.Filters;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IEnrollmentService
{
    Task<Response<GetEnrollmentDto>> CreateEnrollment(CreateEnrollmentDto createEnrollmentDto);
    Task<Response<GetEnrollmentDto>> UpdateEnrollment(int id, UpdateEnrollmentDto updateEnrollmentDto);
    Task<Response<string>> DeleteEnrollment(int id);
    Task<Response<List<GetEnrollmentDto>>> GetEnrollments(EnrollmentFilter filter);
    Task<Response<GetEnrollmentDto>> GetEnrollmentById(int id);
}
