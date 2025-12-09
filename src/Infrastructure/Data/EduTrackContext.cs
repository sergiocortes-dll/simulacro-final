using Microsoft.EntityFrameworkCore;
using EduTrack.Domain.Entities;

namespace EduTrack.Infrastructure.Data
{
    // DbContext simple para Entity Framework Core
    public class EduTrackContext : DbContext
    {
        public EduTrackContext(DbContextOptions<EduTrackContext> options)
            : base(options)
        {
        }

        // DbSets para las entidades
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<ProgramaAcademico> ProgramasAcademicos { get; set; }
        public DbSet<Modalidad> Modalidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones simples de las entidades
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Documento).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Estado).HasMaxLength(20);
                entity.Property(e => e.Telefono).HasMaxLength(20);
                
                // Relaciones
                entity.HasOne(e => e.ProgramaAcademico)
                    .WithMany(p => p.Estudiantes)
                    .HasForeignKey(e => e.ProgramaAcademicoId);
                    
                entity.HasOne(e => e.Modalidad)
                    .WithMany(m => m.Estudiantes)
                    .HasForeignKey(e => e.ModalidadId);
            });

            modelBuilder.Entity<ProgramaAcademico>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Codigo).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Nivel).HasMaxLength(50);
            });

            modelBuilder.Entity<Modalidad>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Nombre).IsRequired().HasMaxLength(100);
            });

            // Seed data inicial para pruebas
            modelBuilder.Entity<Modalidad>().HasData(
                new Modalidad { Id = 1, Nombre = "Presencial", Descripcion = "Clases presenciales" },
                new Modalidad { Id = 2, Nombre = "Virtual", Descripcion = "Clases virtuales online" },
                new Modalidad { Id = 3, Nombre = "Híbrida", Descripcion = "Combinación presencial y virtual" }
            );

            modelBuilder.Entity<ProgramaAcademico>().HasData(
                new ProgramaAcademico 
                { 
                    Id = 1, 
                    Nombre = "Ingeniería de Software", 
                    Codigo = "IS-001", 
                    Descripcion = "Programa de ingeniería enfocado en desarrollo de software",
                    DuracionSemestres = 10,
                    Nivel = "Profesional"
                },
                new ProgramaAcademico 
                { 
                    Id = 2, 
                    Nombre = "Tecnología en Desarrollo de Aplicaciones", 
                    Codigo = "TDA-002", 
                    Descripcion = "Tecnología profesional en desarrollo",
                    DuracionSemestres = 6,
                    Nivel = "Técnico"
                },
                new ProgramaAcademico 
                { 
                    Id = 3, 
                    Nombre = "Maestría en Ciencias de la Computación", 
                    Codigo = "MCC-003", 
                    Descripcion = "Posgrado en ciencias de la computación",
                    DuracionSemestres = 4,
                    Nivel = "Maestría"
                }
            );
        }
    }
}