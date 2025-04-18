using Domain.Dtos.CourseAssignments;
using Domain.Filters;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface ICourseAssignmentService
{
    Task<Response<GetCourseAssignmentDto>> CreateCourseAssignment(CreateCourseAssignmentDto createCourseAssignmentDto);
    Task<Response<GetCourseAssignmentDto>> UpdateCourseAssignment(int id, UpdateCourseAssignmentDto updateCourseAssignmentDto);
    Task<Response<string>> DeleteCourseAssignment(int id);
    Task<Response<List<GetCourseAssignmentDto>>> GetCourseAssignments(CourseAssignmentFilter filter);
    Task<Response<GetCourseAssignmentDto>> GetCourseAssignmentById(int id);
}
