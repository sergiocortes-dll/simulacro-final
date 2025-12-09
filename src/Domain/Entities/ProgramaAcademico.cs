using System.Collections.Generic;

namespace EduTrack.Domain.Entities
{
    public class ProgramaAcademico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int DuracionSemestres { get; set; }
        public string Nivel { get; set; } // Técnico, Profesional, Maestría
        public bool Activo { get; set; }
        
        // Relaciones
        public ICollection<Estudiante> Estudiantes { get; set; }
        
        public ProgramaAcademico()
        {
            Estudiantes = new List<Estudiante>();
            Activo = true;
        }
    }
}