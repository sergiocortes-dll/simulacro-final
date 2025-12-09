namespace EduTrack.Core.Entities
{
    public class TipoDocumento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty; // CC, TI, CE, PAS, etc.
        public bool Estado { get; set; } = true;
        
        // Relaciones
        public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
    }
}