namespace Domain.Entities;

public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }

    public List<Enrollment> Enrollments { get; set; }
}
