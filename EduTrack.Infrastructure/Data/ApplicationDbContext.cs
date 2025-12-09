using EduTrack.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<ProgramaAcademico> ProgramasAcademicos { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Modalidad> Modalidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones de entidades
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NumeroDocumento).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.NumeroDocumento).IsUnique();

                entity.HasOne(e => e.ProgramaAcademico)
                    .WithMany(p => p.Estudiantes)
                    .HasForeignKey(e => e.ProgramaAcademicoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.TipoDocumento)
                    .WithMany(t => t.Estudiantes)
                    .HasForeignKey(e => e.TipoDocumentoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Modalidad)
                    .WithMany(m => m.Estudiantes)
                    .HasForeignKey(e => e.ModalidadId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProgramaAcademico>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Codigo).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(200);
                entity.HasIndex(p => p.Codigo).IsUnique();
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Codigo).IsRequired().HasMaxLength(10);
                entity.Property(t => t.Nombre).IsRequired().HasMaxLength(50);
                entity.HasIndex(t => t.Codigo).IsUnique();
            });

            modelBuilder.Entity<Modalidad>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Codigo).IsRequired().HasMaxLength(20);
                entity.Property(m => m.Nombre).IsRequired().HasMaxLength(50);
                entity.HasIndex(m => m.Codigo).IsUnique();
            });

            // Datos iniciales
            modelBuilder.Entity<TipoDocumento>().HasData(
                new TipoDocumento { Id = 1, Codigo = "CC", Nombre = "Cédula de Ciudadanía" },
                new TipoDocumento { Id = 2, Codigo = "TI", Nombre = "Tarjeta de Identidad" },
                new TipoDocumento { Id = 3, Codigo = "CE", Nombre = "Cédula de Extranjería" },
                new TipoDocumento { Id = 4, Codigo = "PAS", Nombre = "Pasaporte" }
            );

            modelBuilder.Entity<Modalidad>().HasData(
                new Modalidad { Id = 1, Codigo = "PRESENCIAL", Nombre = "Presencial" },
                new Modalidad { Id = 2, Codigo = "VIRTUAL", Nombre = "Virtual" },
                new Modalidad { Id = 3, Codigo = "MIXTA", Nombre = "Mixta" }
            );

            modelBuilder.Entity<ProgramaAcademico>().HasData(
                new ProgramaAcademico { Id = 1, Codigo = "ING-SIS", Nombre = "Ingeniería de Sistemas", Descripcion = "Programa de Ingeniería de Sistemas", DuracionSemestres = 10, Nivel = "Profesional" },
                new ProgramaAcademico { Id = 2, Codigo = "ADM-EMP", Nombre = "Administración de Empresas", Descripcion = "Programa de Administración de Empresas", DuracionSemestres = 8, Nivel = "Profesional" },
                new ProgramaAcademico { Id = 3, Codigo = "CON-ADMON", Nombre = "Contaduría Pública", Descripcion = "Programa de Contaduría Pública", DuracionSemestres = 8, Nivel = "Profesional" },
                new ProgramaAcademico { Id = 4, Codigo = "PSI-CLIN", Nombre = "Psicología Clínica", Descripcion = "Programa de Psicología Clínica", DuracionSemestres = 10, Nivel = "Profesional" }
            );
        }
    }
}