using Microsoft.EntityFrameworkCore;
using EduTrack.Domain.Entities;
using EduTrack.Domain.Interfaces;

namespace EduTrack.Infrastructure.Data.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Enrollment?> GetByIdAsync(int id)
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.EnrollmentCourses)
                    .ThenInclude(ec => ec.Course)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .ToListAsync();
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
        }

        public async Task UpdateAsync(Enrollment enrollment)
        {
            _context.Enrollments.Update(enrollment);
        }

        public async Task DeleteAsync(Enrollment enrollment)
        {
            _context.Enrollments.Remove(enrollment);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Enrollments.AnyAsync(e => e.Id == id);
        }

        // Métodos específicos para Enrollment
        public async Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.EnrollmentCourses)
                    .ThenInclude(ec => ec.Course)
                .OrderByDescending(e => e.EnrollmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetByAcademicPeriodAsync(string academicPeriod)
        {
            return await _context.Enrollments
                .Where(e => e.AcademicPeriod == academicPeriod)
                .Include(e => e.Student)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetByStatusAsync(string status)
        {
            return await _context.Enrollments
                .Where(e => e.EnrollmentStatus == status)
                .Include(e => e.Student)
                .ToListAsync();
        }

        public async Task<Enrollment?> GetActiveEnrollmentByStudentAsync(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId && 
                           e.IsActive && 
                           e.EnrollmentStatus == "Matriculado")
                .Include(e => e.EnrollmentCourses)
                    .ThenInclude(ec => ec.Course)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasActiveEnrollmentAsync(int studentId, string academicPeriod)
        {
            return await _context.Enrollments
                .AnyAsync(e => e.StudentId == studentId && 
                              e.AcademicPeriod == academicPeriod && 
                              e.IsActive);
        }

        public async Task<decimal> GetTotalRevenueByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Enrollments
                .Where(e => e.EnrollmentDate >= startDate && 
                           e.EnrollmentDate <= endDate && 
                           e.PaymentStatus == "Pagado")
                .SumAsync(e => e.PaidAmount ?? 0);
        }

        public async Task<int> GetEnrollmentCountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Enrollments
                .CountAsync(e => e.EnrollmentDate >= startDate && 
                                e.EnrollmentDate <= endDate);
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsWithPendingPaymentAsync()
        {
            return await _context.Enrollments
                .Where(e => e.PaymentStatus == "Pendiente" || e.PaymentStatus == "Parcial")
                .Include(e => e.Student)
                .ToListAsync();
        }

        public async Task<bool> UpdatePaymentStatusAsync(int enrollmentId, string status, decimal? paidAmount = null)
        {
            var enrollment = await GetByIdAsync(enrollmentId);
            if (enrollment == null)
                return false;

            enrollment.PaymentStatus = status;
            if (paidAmount.HasValue)
            {
                enrollment.PaidAmount = paidAmount.Value;
                enrollment.PaymentDate = DateTime.Now;
            }

            _context.Enrollments.Update(enrollment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}