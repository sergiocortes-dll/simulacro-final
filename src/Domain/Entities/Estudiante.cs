using System;
using System.Collections.Generic;

namespace EduTrack.Domain.Entities
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Estado { get; set; } // Activo, Inactivo, Graduado
        public int SemestreActual { get; set; }
        public string Telefono { get; set; }
        public string Observaciones { get; set; }
        
        // Relaciones
        public int ProgramaAcademicoId { get; set; }
        public ProgramaAcademico ProgramaAcademico { get; set; }
        
        public int ModalidadId { get; set; }
        public Modalidad Modalidad { get; set; }
        
        // Constructor simple para principiantes
        public Estudiante()
        {
            FechaIngreso = DateTime.Now;
            Estado = "Activo";
            SemestreActual = 1;
        }
        
        // Métodos de dominio simples
        public void ActualizarSemestre(int nuevoSemestre)
        {
            if (nuevoSemestre > 0 && nuevoSemestre <= 12)
            {
                SemestreActual = nuevoSemestre;
            }
        }
        
        public void CambiarEstado(string nuevoEstado)
        {
            var estadosValidos = new[] { "Activo", "Inactivo", "Graduado", "Suspendido" };
            if (estadosValidos.Contains(nuevoEstado))
            {
                Estado = nuevoEstado;
            }
        }
        
        public string ObtenerNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}