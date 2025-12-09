using Microsoft.AspNetCore.Identity;

namespace EduTrack.Core.Entities
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Estado { get; set; } = string.Empty; // Activo, Inactivo, Graduado, etc.
        public int SemestreActual { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        
        // Relaciones
        public int ProgramaAcademicoId { get; set; }
        public ProgramaAcademico ProgramaAcademico { get; set; } = null!;
        public int TipoDocumentoId { get; set; }
        public TipoDocumento TipoDocumento { get; set; } = null!;
        public int ModalidadId { get; set; }
        public Modalidad Modalidad { get; set; } = null!;
    }
}