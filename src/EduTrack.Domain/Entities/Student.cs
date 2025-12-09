namespace EduTrack.Domain.Entities
{
    public class Student : BaseEntity
    {
        // Propiedades de identificación
        public string DocumentNumber { get; set; } = string.Empty;
        public string DocumentType { get; set; } = "CC"; // CC, TI, CE, Pasaporte
        
        // Información personal
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = "No especificado"; // M, F, Otro
        
        // Información de contacto
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? Country { get; set; }
        
        // Información académica
        public int AcademicProgramId { get; set; }
        public AcademicProgram? AcademicProgram { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; } = "Activo"; // Activo, Inactivo, Graduado, Retirado, Suspendido
        public int CurrentSemester { get; set; } = 1;
        public string Modality { get; set; } = "Presencial"; // Presencial, Virtual, Híbrido
        public decimal? GPA { get; set; }
        public int? TotalCreditsApproved { get; set; }
        
        // Información de autenticación
        public string? PasswordHash { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        
        // Información adicional
        public string? Observations { get; set; }
        public bool IsActive { get; set; } = true;
        
        // Navegación
        public virtual ICollection<AcademicRecord> AcademicRecords { get; set; } = new List<AcademicRecord>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        
        // Métodos del dominio (solo lógica esencial)
        public string GetFullName() => $"{FirstName} {LastName}";
        
        public bool IsEligibleForGraduation()
        {
            // Verificación básica - la lógica completa estará en Application
            return Status == "Activo" && CurrentSemester >= 10; // Ejemplo simplificado
        }
        
        public bool CanEnrollInNextSemester()
        {
            return Status == "Activo" && IsActive;
        }
    }
}