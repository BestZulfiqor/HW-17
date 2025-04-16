using AutoMapper;
using Domain.Dtos.CourseAssignments;
using Domain.Dtos.Courses;
using Domain.Dtos.Enrollments;
using Domain.Dtos.Instructors;
using Domain.Dtos.Students;
using Domain.Entities;

namespace Infrastructure.Mapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Student, GetStudentDto>();
        CreateMap<GetStudentDto, Student>();
        CreateMap<CreateStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>();

        CreateMap<Course, GetCourseDto>();
        CreateMap<GetCourseDto, Course>();
        CreateMap<CreateCourseDto, Course>();
        CreateMap<UpdateCourseDto, Course>();

        CreateMap<CourseAssignment, GetCourseAssignmentDto>();
        CreateMap<GetCourseAssignmentDto, CourseAssignment>();
        CreateMap<CreateCourseAssignmentDto, CourseAssignment>();
        CreateMap<UpdateCourseAssignmentDto, CourseAssignment>();

        CreateMap<Instructor, GetInstructorDto>();
        CreateMap<GetInstructorDto, Instructor>();
        CreateMap<CreateInstructorDto, Instructor>();
        CreateMap<UpdateInstructorDto, Instructor>();

        CreateMap<Enrollment, GetEnrollmentDto>();
        CreateMap<GetEnrollmentDto, Enrollment>();
        CreateMap<CreateEnrollmentDto, Enrollment>();
        CreateMap<UpdateEnrollmentDto, Enrollment>();
    }
}