using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using EduTrack.Domain.Interfaces;
using EduTrack.Domain.Entities;

namespace EduTrack.Infrastructure.ExternalServices
{
    public class PdfService : IPdfService
    {
        public PdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<byte[]> GenerateStudentAcademicRecordAsync(int studentId)
        {
            // Este método debería obtener datos reales de la base de datos
            // Por ahora generamos un PDF de ejemplo
            
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .AlignCenter()
                        .Text("Record Académico")
                        .SemiBold().FontSize(24).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Item().Text($"Estudiante: Juan Pérez");
                            column.Item().Text($"Documento: 12345678");
                            column.Item().Text($"Programa: Ingeniería de Sistemas");
                            column.Item().Text($"Semestre Actual: 5");
                            column.Item().Text($"Fecha de Generación: {DateTime.Now:dd/MM/yyyy HH:mm}");
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Página ");
                            x.CurrentPageNumber();
                            x.Span(" de ");
                            x.TotalPages();
                        });
                });
            });

            return document.GeneratePdf();
        }

        public async Task<byte[]> GenerateStudentsReportAsync(DateTime? startDate, DateTime? endDate)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);

                    page.Header()
                        .AlignCenter()
                        .Text("Reporte de Estudiantes")
                        .SemiBold().FontSize(20);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Documento");
                                header.Cell().Text("Nombre");
                                header.Cell().Text("Programa");
                                header.Cell().Text("Estado");
                                header.Cell().Text("Semestre");
                            });

                            // Datos de ejemplo
                            var sampleData = new[]
                            {
                                new { Doc = "12345678", Name = "Juan Pérez", Program = "Ingeniería", Status = "Activo", Semester = "5" },
                                new { Doc = "87654321", Name = "María García", Program = "Medicina", Status = "Activo", Semester = "3" },
                            };

                            foreach (var item in sampleData)
                            {
                                table.Cell().Text(item.Doc);
                                table.Cell().Text(item.Name);
                                table.Cell().Text(item.Program);
                                table.Cell().Text(item.Status);
                                table.Cell().Text(item.Semester);
                            }
                        });
                });
            });

            return document.GeneratePdf();
        }

        public async Task<string> SavePdfToStorageAsync(byte[] pdfData, string fileName)
        {
            var filePath = Path.Combine("wwwroot", "pdfs", fileName);
            var directory = Path.GetDirectoryName(filePath);
            
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            await File.WriteAllBytesAsync(filePath, pdfData);
            return filePath;
        }
    }
}