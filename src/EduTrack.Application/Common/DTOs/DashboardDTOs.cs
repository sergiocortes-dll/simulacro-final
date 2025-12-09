using System.Collections.Generic;

namespace EduTrack.Application.Common.DTOs
{
    public class DashboardMetricsDto
    {
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public int InactiveStudents { get; set; }
        public int GraduatedStudents { get; set; }
        public int VirtualModalityStudents { get; set; }
        public int InPersonModalityStudents { get; set; }
        public int HybridModalityStudents { get; set; }
        public decimal AverageGPA { get; set; }
        public int NewStudentsThisMonth { get; set; }
        public decimal NewStudentsGrowth { get; set; } // Porcentaje
        
        // Distribución por programa
        public List<ProgramDistributionDto> StudentsByProgram { get; set; } = new();
        
        // Distribución por semestre
        public List<SemesterDistributionDto> StudentsBySemester { get; set; } = new();
        
        // Distribución por estado
        public List<StatusDistributionDto> StudentsByStatus { get; set; } = new();
        
        // Tendencias mensuales
        public List<MonthlyTrendDto> MonthlyEnrollmentTrend { get; set; } = new();
    }

    public class ProgramDistributionDto
    {
        public string ProgramName { get; set; } = string.Empty;
        public string ProgramCode { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public decimal Percentage { get; set; }
    }

    public class SemesterDistributionDto
    {
        public int Semester { get; set; }
        public int StudentCount { get; set; }
        public decimal Percentage { get; set; }
    }

    public class StatusDistributionDto
    {
        public string Status { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public decimal Percentage { get; set; }
    }

    public class MonthlyTrendDto
    {
        public string Month { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Enrollments { get; set; }
        public int Graduations { get; set; }
    }

    public class NaturalLanguageQueryDto
    {
        public string Question { get; set; } = string.Empty;
        public string? UserContext { get; set; }
    }

    public class NaturalLanguageResponseDto
    {
        public string Answer { get; set; } = string.Empty;
        public string SqlQuery { get; set; } = string.Empty; // Para depuración y auditoría
        public object? Data { get; set; }
        public string QueryType { get; set; } = string.Empty; // Métrica, Lista, Detalle, etc.
        public DateTime GeneratedAt { get; set; }
        public double ProcessingTimeMs { get; set; }
    }

    public class AIConfigurationDto
    {
        public string Provider { get; set; } = string.Empty; // OpenAI, Azure, etc.
        public string Model { get; set; } = string.Empty;
        public double Temperature { get; set; } = 0.7;
        public int MaxTokens { get; set; } = 500;
        public List<string> AllowedQueryTypes { get; set; } = new();
    }
}