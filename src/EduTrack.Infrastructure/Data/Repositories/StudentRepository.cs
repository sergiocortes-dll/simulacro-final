using Microsoft.EntityFrameworkCore;
using EduTrack.Domain.Entities;
using EduTrack.Domain.Interfaces;

namespace EduTrack.Infrastructure.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.AcademicProgram)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.AcademicProgram)
                .ToListAsync();
        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Students.Remove(student);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(s => s.Id == id);
        }

        // Métodos específicos para Student
        public async Task<Student?> GetByDocumentNumberAsync(string documentNumber)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.DocumentNumber == documentNumber);
        }

        public async Task<Student?> GetByEmailAsync(string email)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<IEnumerable<Student>> GetByProgramIdAsync(int programId)
        {
            return await _context.Students
                .Where(s => s.AcademicProgramId == programId)
                .Include(s => s.AcademicProgram)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetByStatusAsync(string status)
        {
            return await _context.Students
                .Where(s => s.Status == status)
                .Include(s => s.AcademicProgram)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetBySemesterAsync(int semester)
        {
            return await _context.Students
                .Where(s => s.CurrentSemester == semester)
                .Include(s => s.AcademicProgram)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetByModalityAsync(string modality)
        {
            return await _context.Students
                .Where(s => s.Modality == modality)
                .Include(s => s.AcademicProgram)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Students.CountAsync();
        }

        public async Task<int> GetActiveCountAsync()
        {
            return await _context.Students
                .CountAsync(s => s.IsActive && s.Status == "Activo");
        }

        public async Task<IEnumerable<Student>> SearchAsync(string searchTerm)
        {
            return await _context.Students
                .Where(s => s.DocumentNumber.Contains(searchTerm) ||
                           s.FirstName.Contains(searchTerm) ||
                           s.LastName.Contains(searchTerm) ||
                           s.Email.Contains(searchTerm))
                .Include(s => s.AcademicProgram)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsEnrolledBetweenDatesAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Students
                .Where(s => s.EnrollmentDate >= startDate && s.EnrollmentDate <= endDate)
                .Include(s => s.AcademicProgram)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsWithLowGPAAsync(decimal threshold)
        {
            return await _context.Students
                .Where(s => s.GPA.HasValue && s.GPA.Value < threshold)
                .Include(s => s.AcademicProgram)
                .ToListAsync();
        }
    }
}