using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EduTrack.Domain.Entities;

namespace EduTrack.Infrastructure.Data.Configurations
{
    public class AcademicRecordConfiguration : IEntityTypeConfiguration<AcademicRecord>
    {
        public void Configure(EntityTypeBuilder<AcademicRecord> builder)
        {
            builder.ToTable("AcademicRecords");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.AcademicPeriod)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(r => r.Grade)
                .HasPrecision(3, 2);

            builder.Property(r => r.Status)
                .HasMaxLength(50)
                .HasDefaultValue("En curso");

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relaciones
            builder.HasOne(r => r.Student)
                .WithMany(s => s.AcademicRecords)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Course)
                .WithMany(c => c.AcademicRecords)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}