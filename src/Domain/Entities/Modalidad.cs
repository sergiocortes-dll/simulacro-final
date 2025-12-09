using System.Collections.Generic;

namespace EduTrack.Domain.Entities
{
    public class Modalidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activa { get; set; }
        
        // Relaciones
        public ICollection<Estudiante> Estudiantes { get; set; }
        
        public Modalidad()
        {
            Estudiantes = new List<Estudiante>();
            Activa = true;
        }
    }
}