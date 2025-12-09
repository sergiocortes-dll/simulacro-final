namespace EduTrack.Core.Dtos
{
    public class EstudianteDto
    {
        public int Id { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int SemestreActual { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public string ProgramaAcademico { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string Modalidad { get; set; } = string.Empty;
    }

    public class CrearEstudianteDto
    {
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public int ProgramaAcademicoId { get; set; }
        public int TipoDocumentoId { get; set; }
        public int ModalidadId { get; set; }
        public string Observaciones { get; set; } = string.Empty;
    }

    public class ActualizarEstudianteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public int ProgramaAcademicoId { get; set; }
        public int ModalidadId { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int SemestreActual { get; set; }
        public string Observaciones { get; set; } = string.Empty;
    }
}