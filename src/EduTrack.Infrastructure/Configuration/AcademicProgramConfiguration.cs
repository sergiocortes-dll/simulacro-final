using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EduTrack.Domain.Entities;

namespace EduTrack.Infrastructure.Data.Configurations
{
    public class AcademicProgramConfiguration : IEntityTypeConfiguration<AcademicProgram>
    {
        public void Configure(EntityTypeBuilder<AcademicProgram> builder)
        {
            builder.ToTable("AcademicPrograms");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Faculty)
                .HasMaxLength(100);

            builder.Property(p => p.Modality)
                .HasMaxLength(50)
                .HasDefaultValue("Presencial");

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(p => p.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
        }
    }
}