namespace Domain.Dtos.Enrollments;

public class CreateEnrollmentDto
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public int Grade { get; set; }
}
