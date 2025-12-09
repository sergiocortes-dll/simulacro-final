namespace EduTrack.Core.Entities
{
    public class Modalidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty; // PRESENCIAL, VIRTUAL, MIXTA
        public bool Estado { get; set; } = true;
        
        // Relaciones
        public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
    }
}