namespace EduTrack.Domain.Entities
{
    public class Course : BaseEntity
    {
        // Identificación
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // Información académica
        public int AcademicProgramId { get; set; }
        public AcademicProgram? AcademicProgram { get; set; }
        
        public int Credits { get; set; } = 3;
        public int WeeklyHours { get; set; } = 3;
        public string CourseType { get; set; } = "Obligatoria"; // Obligatoria, Electiva, Optativa
        public int Semester { get; set; } = 1; // Semestre en el que se cursa
        
        // Prerrequisitos
        public string? Prerequisites { get; set; } // IDs de cursos separados por comas
        public string? Corequisites { get; set; } // IDs de cursos separados por comas
        
        // Estado
        public bool IsActive { get; set; } = true;
        public DateTime? LastOfferedDate { get; set; }
        
        // Información del profesor
        public string? ProfessorName { get; set; }
        public string? ProfessorEmail { get; set; }
        
        // Navegación
        public virtual ICollection<AcademicRecord> AcademicRecords { get; set; } = new List<AcademicRecord>();
        
        // Métodos del dominio
        public bool IsCurrentlyOffered()
        {
            return IsActive && (!LastOfferedDate.HasValue || LastOfferedDate.Value > DateTime.Now.AddMonths(-12));
        }
        
        public bool HasPrerequisites()
        {
            return !string.IsNullOrEmpty(Prerequisites);
        }
    }
}