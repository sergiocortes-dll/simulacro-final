namespace EduTrack.Core.Entities
{
    public class ProgramaAcademico
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int DuracionSemestres { get; set; }
        public string Nivel { get; set; } = string.Empty; // Técnico, Profesional, Maestría, etc.
        public bool Estado { get; set; } = true;
        
        // Relaciones
        public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
    }
}