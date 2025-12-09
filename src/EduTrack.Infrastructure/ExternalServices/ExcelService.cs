using OfficeOpenXml;
using EduTrack.Domain.Interfaces;
using EduTrack.Domain.Entities;

namespace EduTrack.Infrastructure.ExternalServices
{
    public class ExcelService : IExcelService
    {
        public ExcelService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<IEnumerable<Student>> ReadStudentsFromExcelAsync(Stream fileStream)
        {
            var students = new List<Student>();

            using (var package = new ExcelPackage(fileStream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Saltar la cabecera
                {
                    var student = new Student
                    {
                        DocumentNumber = worksheet.Cells[row, 1].Text,
                        FirstName = worksheet.Cells[row, 2].Text,
                        LastName = worksheet.Cells[row, 3].Text,
                        Email = worksheet.Cells[row, 4].Text,
                        PhoneNumber = worksheet.Cells[row, 5].Text,
                        BirthDate = DateTime.Parse(worksheet.Cells[row, 6].Text),
                        Address = worksheet.Cells[row, 7].Text,
                        // Nota: AcademicProgramId debería ser resuelto a partir del código
                        EnrollmentDate = DateTime.Parse(worksheet.Cells[row, 9].Text),
                        Status = worksheet.Cells[row, 10].Text,
                        CurrentSemester = int.Parse(worksheet.Cells[row, 11].Text),
                        Modality = worksheet.Cells[row, 12].Text,
                        Observations = worksheet.Cells[row, 13].Text,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    students.Add(student);
                }
            }

            return students;
        }

        public async Task<Stream> GenerateExcelReportAsync(IEnumerable<Student> students)
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Estudiantes");

                // Cabecera
                worksheet.Cells[1, 1].Value = "Documento";
                worksheet.Cells[1, 2].Value = "Nombre";
                worksheet.Cells[1, 3].Value = "Apellido";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Teléfono";
                worksheet.Cells[1, 6].Value = "Programa";
                worksheet.Cells[1, 7].Value = "Estado";
                worksheet.Cells[1, 8].Value = "Semestre";
                worksheet.Cells[1, 9].Value = "Modalidad";

                // Datos
                int row = 2;
                foreach (var student in students)
                {
                    worksheet.Cells[row, 1].Value = student.DocumentNumber;
                    worksheet.Cells[row, 2].Value = student.FirstName;
                    worksheet.Cells[row, 3].Value = student.LastName;
                    worksheet.Cells[row, 4].Value = student.Email;
                    worksheet.Cells[row, 5].Value = student.PhoneNumber;
                    // Nota: Asumiendo que tenemos acceso a AcademicProgram
                    worksheet.Cells[row, 6].Value = "Programa"; // student.AcademicProgram?.Name
                    worksheet.Cells[row, 7].Value = student.Status;
                    worksheet.Cells[row, 8].Value = student.CurrentSemester;
                    worksheet.Cells[row, 9].Value = student.Modality;
                    row++;
                }

                // Autoajustar columnas
                worksheet.Cells[1, 1, row - 1, 9].AutoFitColumns();

                package.Save();
            }

            stream.Position = 0;
            return await Task.FromResult(stream);
        }

        public async Task ValidateExcelStructureAsync(Stream fileStream)
        {
            using (var package = new ExcelPackage(fileStream))
            {
                if (package.Workbook.Worksheets.Count == 0)
                    throw new InvalidOperationException("El archivo Excel no contiene hojas");

                var worksheet = package.Workbook.Worksheets[0];
                
                // Verificar que tenga las columnas esperadas
                var expectedHeaders = new List<string>
                {
                    "DocumentNumber", "FirstName", "LastName", "Email", "PhoneNumber",
                    "BirthDate", "Address", "AcademicProgramCode", "EnrollmentDate",
                    "Status", "CurrentSemester", "Modality", "Observations"
                };

                for (int i = 0; i < expectedHeaders.Count; i++)
                {
                    if (worksheet.Cells[1, i + 1].Text != expectedHeaders[i])
                        throw new InvalidOperationException($"La columna {i + 1} debería ser '{expectedHeaders[i]}'");
                }
            }
        }
    }
}