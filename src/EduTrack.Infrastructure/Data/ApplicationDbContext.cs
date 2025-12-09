using Microsoft.EntityFrameworkCore;
using EduTrack.Domain.Entities;
using EduTrack.Infrastructure.Data.Configurations;

namespace EduTrack.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<AcademicProgram> AcademicPrograms { get; set; }
        public DbSet<AcademicRecord> AcademicRecords { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<EnrollmentCourse> EnrollmentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones de entidades
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new AcademicProgramConfiguration());
            modelBuilder.ApplyConfiguration(new AcademicRecordConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollmentCourseConfiguration());

            // Índices adicionales
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.DocumentNumber)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => new { s.AcademicProgramId, s.Status });

            modelBuilder.Entity<AcademicProgram>()
                .HasIndex(p => p.Code)
                .IsUnique();
        }
    }
}