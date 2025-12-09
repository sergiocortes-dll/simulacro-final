using Microsoft.EntityFrameworkCore;
using EduTrack.Domain.Entities;

namespace EduTrack.Infrastructure.Data.Seeders
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Verificar si ya hay datos
            if (await context.AcademicPrograms.AnyAsync())
                return;

            // Programas académicos de ejemplo
            var programs = new List<AcademicProgram>
            {
                new AcademicProgram
                {
                    Code = "ISIS",
                    Name = "Ingeniería de Sistemas",
                    Description = "Programa de ingeniería de sistemas e informática",
                    DurationInSemesters = 10,
                    TotalCredits = 160,
                    Faculty = "Ingeniería",
                    Modality = "Presencial",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new AcademicProgram
                {
                    Code = "MED",
                    Name = "Medicina",
                    Description = "Programa de medicina general",
                    DurationInSemesters = 12,
                    TotalCredits = 200,
                    Faculty = "Ciencias de la Salud",
                    Modality = "Presencial",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new AcademicProgram
                {
                    Code = "ADM",
                    Name = "Administración de Empresas",
                    Description = "Programa de administración y gestión empresarial",
                    DurationInSemesters = 8,
                    TotalCredits = 140,
                    Faculty = "Ciencias Económicas",
                    Modality = "Virtual",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };

            await context.AcademicPrograms.AddRangeAsync(programs);
            await context.SaveChangesAsync();

            // Cursos de ejemplo
            var courses = new List<Course>
            {
                new Course
                {
                    Code = "ISIS101",
                    Name = "Fundamentos de Programación",
                    Description = "Introducción a la programación",
                    AcademicProgramId = 1, // Ingeniería de Sistemas
                    Credits = 3,
                    WeeklyHours = 4,
                    CourseType = "Obligatoria",
                    Semester = 1,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Course
                {
                    Code = "MED101",
                    Name = "Anatomía Humana",
                    Description = "Estudio de la anatomía humana",
                    AcademicProgramId = 2, // Medicina
                    Credits = 4,
                    WeeklyHours = 5,
                    CourseType = "Obligatoria",
                    Semester = 1,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };

            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }
}