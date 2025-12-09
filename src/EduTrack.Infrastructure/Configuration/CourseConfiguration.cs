using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EduTrack.Domain.Entities;

namespace EduTrack.Infrastructure.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.CourseType)
                .HasMaxLength(50)
                .HasDefaultValue("Obligatoria");

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relaciones
            builder.HasOne(c => c.AcademicProgram)
                .WithMany(p => p.Courses)
                .HasForeignKey(c => c.AcademicProgramId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}