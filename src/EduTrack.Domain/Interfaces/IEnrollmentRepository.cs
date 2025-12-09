using EduTrack.Domain.Entities;

namespace EduTrack.Domain.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        // Métodos específicos para Enrollment
        Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId);
        Task<IEnumerable<Enrollment>> GetByAcademicPeriodAsync(string academicPeriod);
        Task<IEnumerable<Enrollment>> GetByStatusAsync(string status);
        Task<Enrollment?> GetActiveEnrollmentByStudentAsync(int studentId);
        Task<bool> HasActiveEnrollmentAsync(int studentId, string academicPeriod);
        
        // Métodos para reportes
        Task<decimal> GetTotalRevenueByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<int> GetEnrollmentCountByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Enrollment>> GetEnrollmentsWithPendingPaymentAsync();
        
        // Métodos para operaciones específicas
        Task<bool> UpdatePaymentStatusAsync(int enrollmentId, string status, decimal? paidAmount = null);
    }
}