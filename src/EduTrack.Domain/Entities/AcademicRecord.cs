namespace EduTrack.Domain.Entities
{
    public class AcademicRecord : BaseEntity
    {
        // Relaciones
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        
        // Información del registro
        public string AcademicPeriod { get; set; } = string.Empty; // Ej: "2024-1"
        public int Semester { get; set; }
        
        // Calificaciones
        public decimal? Grade { get; set; } // 0.0 - 5.0
        public string? LetterGrade { get; set; } // A, B, C, D, F
        public decimal? Percentage { get; set; } // 0 - 100
        
        // Estado
        public string Status { get; set; } = "En curso"; // En curso, Aprobado, Reprobado, Retirado, Cancelado
        public DateTime? ApprovalDate { get; set; }
        
        // Información adicional
        public int AttemptNumber { get; set; } = 1;
        public bool IsElective { get; set; } = false;
        public string? Observations { get; set; }
        
        // Métodos del dominio
        public bool IsApproved()
        {
            return Status == "Aprobado" && Grade.HasValue && Grade.Value >= 3.0m;
        }
        
        public bool CanRetake()
        {
            return Status == "Reprobado" && AttemptNumber < 3; // Máximo 3 intentos
        }
    }
}