using Xunit;
using Moq;
using EduTrack.Application.Services;
using EduTrack.Domain.Entities;
using EduTrack.Domain.Repositories;

namespace EduTrack.Application.Tests
{
    public class EstudianteServiceTests
    {
        private readonly Mock<IEstudianteRepository> _mockEstudianteRepo;
        private readonly Mock<IProgramaAcademicoRepository> _mockProgramaRepo;
        private readonly EstudianteService _estudianteService;

        public EstudianteServiceTests()
        {
            _mockEstudianteRepo = new Mock<IEstudianteRepository>();
            _mockProgramaRepo = new Mock<IProgramaAcademicoRepository>();
            _estudianteService = new EstudianteService(
                _mockEstudianteRepo.Object,
                _mockProgramaRepo.Object);
        }

        [Fact]
        public async Task CrearEstudiante_ConDatosValidos_DebeCrearCorrectamente()
        {
            // Arrange
            var estudiante = new Estudiante
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juan@email.com",
                Documento = "123456",
                ProgramaAcademicoId = 1
            };

            _mockProgramaRepo.Setup(x => x.ExistsAsync(1))
                .ReturnsAsync(true);
            _mockEstudianteRepo.Setup(x => x.AddAsync(It.IsAny<Estudiante>()))
                .ReturnsAsync(estudiante);

            // Act
            var resultado = await _estudianteService.CrearEstudianteAsync(estudiante);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Juan", resultado.Nombre);
            _mockEstudianteRepo.Verify(x => x.AddAsync(It.IsAny<Estudiante>()), Times.Once);
        }

        [Fact]
        public async Task CrearEstudiante_SinNombre_DebeLanzarExcepcion()
        {
            // Arrange
            var estudiante = new Estudiante
            {
                Apellido = "Pérez",
                Email = "juan@email.com",
                Documento = "123456",
                ProgramaAcademicoId = 1
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => _estudianteService.CrearEstudianteAsync(estudiante));
        }

        [Fact]
        public async Task CrearEstudiante_ProgramaNoExistente_DebeLanzarExcepcion()
        {
            // Arrange
            var estudiante = new Estudiante
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juan@email.com",
                Documento = "123456",
                ProgramaAcademicoId = 999
            };

            _mockProgramaRepo.Setup(x => x.ExistsAsync(999))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => _estudianteService.CrearEstudianteAsync(estudiante));
        }

        [Fact]
        public async Task ObtenerEstudiante_Existente_DebeRetornarEstudiante()
        {
            // Arrange
            var estudiante = new Estudiante
            {
                Id = 1,
                Nombre = "Juan",
                Apellido = "Pérez",
                Email = "juan@email.com"
            };

            _mockEstudianteRepo.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(estudiante);

            // Act
            var resultado = await _estudianteService.ObtenerEstudianteAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            Assert.Equal("Juan", resultado.Nombre);
        }

        [Fact]
        public async Task ObtenerEstudiante_NoExistente_DebeRetornarNull()
        {
            // Arrange
            _mockEstudianteRepo.Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Estudiante)null);

            // Act
            var resultado = await _estudianteService.ObtenerEstudianteAsync(999);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task EliminarEstudiante_Existente_DebeEliminarCorrectamente()
        {
            // Arrange
            _mockEstudianteRepo.Setup(x => x.ExistsAsync(1))
                .ReturnsAsync(true);
            _mockEstudianteRepo.Setup(x => x.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            await _estudianteService.EliminarEstudianteAsync(1);

            // Assert
            _mockEstudianteRepo.Verify(x => x.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task EliminarEstudiante_NoExistente_DebeLanzarExcepcion()
        {
            // Arrange
            _mockEstudianteRepo.Setup(x => x.ExistsAsync(999))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _estudianteService.EliminarEstudianteAsync(999));
        }
    }
}