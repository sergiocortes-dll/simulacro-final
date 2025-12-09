namespace EduTrack.Application.Common.DTOs
{
    // DTO para registro de estudiante
    public class RegisterStudentDto
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public int AcademicProgramId { get; set; }
    }

    // DTO para respuesta de registro
    public class StudentRegistrationResultDto
    {
        public int StudentId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public bool EmailSent { get; set; }
    }

    // DTO para login
    public class StudentLoginDto
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    // DTO para respuesta de autenticación
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public int StudentId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
    }

    // DTO para perfil de estudiante
    public class StudentProfileDto
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public int AcademicProgramId { get; set; }
        public string AcademicProgramName { get; set; } = string.Empty;
        public string AcademicProgramCode { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int CurrentSemester { get; set; }
        public string Modality { get; set; } = string.Empty;
        public decimal? GPA { get; set; }
        public int? TotalCredits { get; set; }
    }

    // DTO para actualización de perfil
    public class UpdateStudentProfileDto
    {
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
    }

    // DTO para cambio de contraseña
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    // DTO para listado de estudiantes
    public class StudentListItemDto
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AcademicProgram { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int CurrentSemester { get; set; }
        public string Modality { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
    }

    // DTO para importación desde Excel
    public class ExcelStudentImportDto
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public string AcademicProgramCode { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int CurrentSemester { get; set; }
        public string Modality { get; set; } = string.Empty;
        public string? Observations { get; set; }
    }

    // DTO para resultado de importación
    public class ExcelImportResultDto
    {
        public int TotalRecords { get; set; }
        public int SuccessfullyImported { get; set; }
        public int Updated { get; set; }
        public int Failed { get; set; }
        public List<string> Errors { get; set; } = new();
        public DateTime ImportDate { get; set; }
        public string ImportedBy { get; set; } = string.Empty;
    }

    // DTO para información de PDF
    public class PdfStudentInfoDto
    {
        public int StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AcademicProgram { get; set; } = string.Empty;
        public string ProgramCode { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int CurrentSemester { get; set; }
        public string Modality { get; set; } = string.Empty;
        public decimal? GPA { get; set; }
        public int TotalApprovedSubjects { get; set; }
        public List<AcademicRecordDto> AcademicRecords { get; set; } = new();
        public List<string> Observations { get; set; } = new();
        public DateTime GenerationDate { get; set; }
    }

    public class AcademicRecordDto
    {
        public string Semester { get; set; } = string.Empty;
        public string SubjectCode { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public int Credits { get; set; }
        public decimal Grade { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? ApprovalDate { get; set; }
    }
}