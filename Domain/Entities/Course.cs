namespace Domain.Entities;

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public List<Enrollment> Enrollments { get; set; }
    public List<CourseAssignment> CourseAssignments { get; set; }
}
