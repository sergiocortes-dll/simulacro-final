using EduTrack.Application.Common.DTOs;

namespace EduTrack.Application.Common.Interfaces
{
    public interface IPdfService
    {
        Task<byte[]> GenerateStudentAcademicRecordPdfAsync(PdfStudentInfoDto studentInfo);
        Task<byte[]> GenerateStudentsReportPdfAsync(List<StudentListItemDto> students, ReportType reportType);
        Task<string> GeneratePdfAndSaveAsync(PdfStudentInfoDto studentInfo, string outputPath);
    }

    public enum ReportType
    {
        AllStudents,
        ByProgram,
        ByStatus,
        BySemester
    }
}