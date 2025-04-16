using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<CourseAssignment> CourseAssignments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enrollment>()
            .HasOne(n => n.Student)
            .WithMany(n => n.Enrollments)
            .HasForeignKey(n => n.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);

        modelBuilder.Entity<CourseAssignment>()
            .HasOne(ca => ca.Instructor)
            .WithMany(i => i.CourseAssignments)
            .HasForeignKey(ca => ca.InstructorId);

        modelBuilder.Entity<CourseAssignment>()
           .HasOne(ca => ca.Course)
           .WithMany(c => c.CourseAssignments)
           .HasForeignKey(ca => ca.CourseId);
    }
}
