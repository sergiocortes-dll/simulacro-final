using OpenAI_API;
using OpenAI_API.Completions;
using EduTrack.Domain.Interfaces;

namespace EduTrack.Infrastructure.ExternalServices
{
    public class OpenAIService : IAIAnalyticsService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OpenAIService> _logger;
        private OpenAIAPI? _openAiApi;

        public OpenAIService(IConfiguration configuration, ILogger<OpenAIService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            
            var apiKey = _configuration["OpenAI:ApiKey"];
            if (!string.IsNullOrEmpty(apiKey))
            {
                _openAiApi = new OpenAIAPI(apiKey);
            }
        }

        public async Task<string> ProcessNaturalLanguageQueryAsync(string query)
        {
            if (_openAiApi == null)
            {
                _logger.LogWarning("OpenAI API no configurada");
                return "Servicio de IA no disponible. Por favor, contacte al administrador.";
            }

            try
            {
                var completionRequest = new CompletionRequest
                {
                    Prompt = $"Eres un asistente para un sistema de gestión estudiantil. El usuario pregunta: {query}\n\nResponde de manera útil y concisa:",
                    Model = _configuration["OpenAI:Model"] ?? "text-davinci-003",
                    MaxTokens = 500,
                    Temperature = 0.7
                };

                var result = await _openAiApi.Completions.CreateCompletionAsync(completionRequest);
                
                if (result.Completions.Count > 0)
                {
                    return result.Completions[0].Text.Trim();
                }

                return "No pude generar una respuesta. Intenta reformular tu pregunta.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error procesando consulta con OpenAI");
                return "Ocurrió un error al procesar tu consulta. Por favor, intenta más tarde.";
            }
        }

        public async Task<object> GetDashboardAnalyticsAsync()
        {
            // Ejemplo de cómo la IA podría analizar datos
            var analysis = new
            {
                TotalStudents = 1500,
                ActiveStudents = 1200,
                AverageGPA = 3.8,
                TopProgram = "Ingeniería de Sistemas",
                EnrollmentTrend = "Creciente",
                Recommendations = new[]
                {
                    "Incrementar programas virtuales",
                    "Mejorar retención en primeros semestres",
                    "Ofrecer más electivas técnicas"
                }
            };

            return await Task.FromResult(analysis);
        }

        public async Task<string> GenerateStudentPerformanceAnalysisAsync(int studentId)
        {
            // Ejemplo de análisis de rendimiento
            var analysis = $"""
                Análisis de rendimiento para el estudiante ID: {studentId}
                
                Resumen:
                - GPA actual: 3.8
                - Asignaturas aprobadas: 25/30
                - Fortalezas: Matemáticas, Programación
                - Áreas de mejora: Inglés, Comunicación
                
                Recomendaciones:
                1. Tomar tutorías en inglés
                2. Participar en clubes de programación
                3. Considerar electivas en inteligencia artificial
                
                Proyección:
                - Probabilidad de graduación: 95%
                - Tiempo estimado: 5 semestres
                """;

            return await Task.FromResult(analysis);
        }
    }
}