using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EduTrack.Domain.Entities;

namespace EduTrack.Infrastructure.Data.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollments");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.AcademicPeriod)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.EnrollmentDate)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.PaymentDate)
                .IsRequired(false);

            builder.Property(e => e.TuitionAmount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(e => e.DiscountAmount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(e => e.PaidAmount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pendiente");

            builder.Property(e => e.PaymentMethod)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.EnrollmentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Matriculado");

            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);

            builder.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(e => e.PaymentReceiptUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

            // Relaciones
            builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices
            builder.HasIndex(e => e.StudentId);
            builder.HasIndex(e => e.AcademicPeriod);
            builder.HasIndex(e => e.EnrollmentStatus);
            builder.HasIndex(e => e.PaymentStatus);
            builder.HasIndex(e => new { e.EnrollmentDate, e.IsActive });

            // Configuración para el campo Observations
            builder.Property(e => e.Observations)
                .HasColumnType("text")
                .IsRequired(false);

            // Configuración de valores por defecto adicionales
            builder.Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}