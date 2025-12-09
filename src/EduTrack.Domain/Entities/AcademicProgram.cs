namespace EduTrack.Domain.Entities
{
    public class AcademicProgram : BaseEntity
    {
        // Propiedades básicas
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // Información académica
        public int DurationInSemesters { get; set; } = 10;
        public int TotalCredits { get; set; } = 160;
        public string Faculty { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Modality { get; set; } = "Presencial"; // Presencial, Virtual, Híbrido
        
        // Requisitos
        public decimal? MinimumGPA { get; set; }
        public int? MinimumCreditsPerSemester { get; set; }
        public int? MaximumCreditsPerSemester { get; set; }
        
        // Estado
        public bool IsActive { get; set; } = true;
        public DateTime? AccreditationExpiryDate { get; set; }
        public string AccreditationStatus { get; set; } = "Acreditado";
        
        // Información adicional
        public string? CoordinatorName { get; set; }
        public string? CoordinatorEmail { get; set; }
        public string? StudyPlanUrl { get; set; }
        
        // Navegación
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        
        // Métodos del dominio
        public bool IsCurrentlyAccredited()
        {
            if (AccreditationExpiryDate.HasValue)
                return AccreditationExpiryDate.Value > DateTime.Now;
            
            return AccreditationStatus == "Acreditado";
        }
        
        public bool CanAcceptNewStudents()
        {
            return IsActive && IsCurrentlyAccredited();
        }
    }
}