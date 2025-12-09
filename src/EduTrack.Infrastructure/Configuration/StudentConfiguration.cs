using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EduTrack.Domain.Entities;

namespace EduTrack.Infrastructure.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.DocumentNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.DocumentType)
                .HasMaxLength(10)
                .HasDefaultValue("CC");

            builder.Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(s => s.Address)
                .HasMaxLength(500);

            builder.Property(s => s.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Activo");

            builder.Property(s => s.Modality)
                .HasMaxLength(50)
                .HasDefaultValue("Presencial");

            builder.Property(s => s.GPA)
                .HasPrecision(3, 2);

            builder.Property(s => s.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(s => s.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

            // Relaciones
            builder.HasOne(s => s.AcademicProgram)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.AcademicProgramId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}