using EduTrack.Application.Common.DTOs;
using EduTrack.Domain.Entities;

namespace EduTrack.Application.Common.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> GetByDocumentNumberAsync(string documentNumber);
        Task<Student?> GetByEmailAsync(string email);
        Task<List<Student>> GetAllAsync();
        Task<List<Student>> GetByProgramIdAsync(int programId);
        Task<List<Student>> GetByStatusAsync(string status);
        Task<List<Student>> GetBySemesterAsync(int semester);
        Task<int> AddAsync(Student student);
        Task<bool> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByDocumentNumberAsync(string documentNumber);
        Task<bool> ExistsByEmailAsync(string email);
        Task<int> GetTotalCountAsync();
        Task<int> GetActiveCountAsync();
        Task<List<StudentListItemDto>> GetStudentListAsync(StudentFilterDto? filter = null);
    }

    public class StudentFilterDto
    {
        public string? SearchTerm { get; set; }
        public int? ProgramId { get; set; }
        public string? Status { get; set; }
        public int? Semester { get; set; }
        public string? Modality { get; set; }
        public DateTime? EnrollmentDateFrom { get; set; }
        public DateTime? EnrollmentDateTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
    }
}