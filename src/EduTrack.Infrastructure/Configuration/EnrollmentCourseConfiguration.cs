using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EduTrack.Domain.Entities;

namespace EduTrack.Infrastructure.Data.Configurations
{
    public class EnrollmentCourseConfiguration : IEntityTypeConfiguration<EnrollmentCourse>
    {
        public void Configure(EntityTypeBuilder<EnrollmentCourse> builder)
        {
            builder.ToTable("EnrollmentCourses");

            builder.HasKey(ec => ec.Id);

            // Configuración de propiedades
            builder.Property(ec => ec.Section)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("01");

            builder.Property(ec => ec.Schedule)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(ec => ec.Classroom)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(ec => ec.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Inscrito");

            builder.Property(ec => ec.WithdrawalDate)
                .IsRequired(false);

            builder.Property(ec => ec.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(ec => ec.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

            // Configuración de la clave compuesta (opcional)
            builder.HasIndex(ec => new { ec.EnrollmentId, ec.CourseId })
                .IsUnique();

            // Relaciones
            builder.HasOne(ec => ec.Enrollment)
                .WithMany(e => e.EnrollmentCourses)
                .HasForeignKey(ec => ec.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ec => ec.Course)
                .WithMany() // EnrollmentCourse no está mapeado en Course
                .HasForeignKey(ec => ec.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices adicionales
            builder.HasIndex(ec => ec.EnrollmentId);
            builder.HasIndex(ec => ec.CourseId);
            builder.HasIndex(ec => ec.Status);
            builder.HasIndex(ec => new { ec.CourseId, ec.Section });

            // Configuración de validaciones adicionales
            builder.Property(ec => ec.Section)
                .HasAnnotation("MinLength", 1)
                .HasAnnotation("RegularExpression", "^[A-Z0-9]+$");

            // Configuración para el campo Schedule
            builder.Property(ec => ec.Schedule)
                .HasConversion(
                    v => v ?? string.Empty,
                    v => string.IsNullOrEmpty(v) ? null : v)
                .HasMaxLength(200);

            // Configuración para el campo Classroom
            builder.Property(ec => ec.Classroom)
                .HasConversion(
                    v => v ?? string.Empty,
                    v => string.IsNullOrEmpty(v) ? null : v)
                .HasMaxLength(50);

            // Configuración para manejar el estado de retiro
            builder.HasCheckConstraint(
                "CK_EnrollmentCourses_WithdrawalDate",
                $"[WithdrawalDate] IS NULL OR [WithdrawalDate] >= [CreatedAt]");

            // Configuración para ignorar métodos computados
            builder.Ignore(ec => ec.IsCurrentlyEnrolled);
        }
    }
}