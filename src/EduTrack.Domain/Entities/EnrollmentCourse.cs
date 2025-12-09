namespace EduTrack.Domain.Entities
{
    public class EnrollmentCourse : BaseEntity
    {
        // Relaciones
        public int EnrollmentId { get; set; }
        public Enrollment? Enrollment { get; set; }
        
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        
        // Información específica
        public string Section { get; set; } = "01";
        public string? Schedule { get; set; } // "Lunes 8:00-10:00, Miércoles 8:00-10:00"
        public string? Classroom { get; set; }
        
        // Estado
        public string Status { get; set; } = "Inscrito"; // Inscrito, Retirado, Aprobado, Reprobado
        public DateTime? WithdrawalDate { get; set; }
        
        // Métodos del dominio
        public bool IsCurrentlyEnrolled()
        {
            return Status == "Inscrito" && !WithdrawalDate.HasValue;
        }
        
        public bool CanWithdraw()
        {
            // Lógica simple: se puede retirar si está inscrito y no ha pasado mucho tiempo
            return Status == "Inscrito";
        }
    }
}