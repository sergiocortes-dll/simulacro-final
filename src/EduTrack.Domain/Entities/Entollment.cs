namespace EduTrack.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        // Relaciones
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        
        // Información de la matrícula
        public string AcademicPeriod { get; set; } = string.Empty; // Ej: "2024-1"
        public DateTime EnrollmentDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        
        // Estado financiero
        public decimal TuitionAmount { get; set; } = 0;
        public decimal? DiscountAmount { get; set; } = 0;
        public decimal? PaidAmount { get; set; } = 0;
        public string PaymentStatus { get; set; } = "Pendiente"; // Pendiente, Pagado, Parcial, Cancelado
        public string? PaymentMethod { get; set; }
        public string? TransactionId { get; set; }
        
        // Estado académico
        public string EnrollmentStatus { get; set; } = "Matriculado"; // Matriculado, Retirado, Cancelado, Congelado
        public bool IsActive { get; set; } = true;
        
        // Información adicional
        public string? InvoiceNumber { get; set; }
        public string? PaymentReceiptUrl { get; set; }
        public string? Observations { get; set; }
        
        // Navegación
        public virtual ICollection<EnrollmentCourse> EnrollmentCourses { get; set; } = new List<EnrollmentCourse>();
        
        // Métodos del dominio
        public decimal GetTotalAmount()
        {
            return TuitionAmount - (DiscountAmount ?? 0);
        }
        
        public decimal GetPendingAmount()
        {
            var total = GetTotalAmount();
            return total - (PaidAmount ?? 0);
        }
        
        public bool IsFullyPaid()
        {
            return GetPendingAmount() <= 0;
        }
        
        public bool CanEnrollInCourses()
        {
            return EnrollmentStatus == "Matriculado" && IsActive && IsFullyPaid();
        }
    }
}