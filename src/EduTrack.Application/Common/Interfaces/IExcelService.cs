using EduTrack.Application.Common.DTOs;
using Microsoft.AspNetCore.Http;

namespace EduTrack.Application.Common.Interfaces
{
    public interface IExcelService
    {
        Task<List<ExcelStudentImportDto>> ReadStudentsFromExcelAsync(IFormFile file);
        Task<ExcelImportResultDto> ImportStudentsFromExcelAsync(IFormFile file, string importedBy);
        Task<byte[]> GenerateExcelReportAsync(List<StudentListItemDto> students);
        bool ValidateExcelStructure(IFormFile file);
        List<string> ValidateStudentData(ExcelStudentImportDto studentData);
    }
}