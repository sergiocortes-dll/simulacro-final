using EduTrack.Application.Common.DTOs;

namespace EduTrack.Application.Common.Interfaces
{
    public interface IAIAnalyticsService
    {
        Task<NaturalLanguageResponseDto> ProcessNaturalLanguageQueryAsync(NaturalLanguageQueryDto query);
        Task<string> GenerateSqlFromNaturalLanguageAsync(string naturalLanguageQuery);
        Task<DashboardMetricsDto> GetDashboardMetricsAsync();
        Task<List<string>> GetSuggestedQuestionsAsync();
        Task<string> AnalyzeStudentPerformanceAsync(int studentId);
    }
}