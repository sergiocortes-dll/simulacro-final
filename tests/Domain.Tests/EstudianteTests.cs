using Xunit;
using EduTrack.Domain.Entities;

namespace EduTrack.Domain.Tests
{
    public class EstudianteTests
    {
        [Fact]
        public void CrearEstudiante_ConValoresPorDefecto_DebeInicializarCorrectamente()
        {
            // Arrange & Act
            var estudiante = new Estudiante();
            
            // Assert
            Assert.NotNull(estudiante);
            Assert.Equal("Activo", estudiante.Estado);
            Assert.Equal(1, estudiante.SemestreActual);
            Assert.True(estudiante.FechaIngreso <= System.DateTime.Now);
        }

        [Fact]
        public void ActualizarSemestre_ConValorValido_DebeActualizarCorrectamente()
        {
            // Arrange
            var estudiante = new Estudiante();
            var nuevoSemestre = 3;
            
            // Act
            estudiante.ActualizarSemestre(nuevoSemestre);
            
            // Assert
            Assert.Equal(nuevoSemestre, estudiante.SemestreActual);
        }

        [Fact]
        public void ActualizarSemestre_ConValorInvalido_NoDebeActualizar()
        {
            // Arrange
            var estudiante = new Estudiante();
            var semestreInicial = estudiante.SemestreActual;
            var semestreInvalido = -1;
            
            // Act
            estudiante.ActualizarSemestre(semestreInvalido);
            
            // Assert
            Assert.Equal(semestreInicial, estudiante.SemestreActual);
        }

        [Fact]
        public void CambiarEstado_ConEstadoValido_DebeCambiarCorrectamente()
        {
            // Arrange
            var estudiante = new Estudiante();
            var nuevoEstado = "Graduado";
            
            // Act
            estudiante.CambiarEstado(nuevoEstado);
            
            // Assert
            Assert.Equal(nuevoEstado, estudiante.Estado);
        }

        [Fact]
        public void CambiarEstado_ConEstadoInvalido_NoDebeCambiar()
        {
            // Arrange
            var estudiante = new Estudiante();
            var estadoInicial = estudiante.Estado;
            var estadoInvalido = "EstadoNoValido";
            
            // Act
            estudiante.CambiarEstado(estadoInvalido);
            
            // Assert
            Assert.Equal(estadoInicial, estudiante.Estado);
        }

        [Fact]
        public void ObtenerNombreCompleto_DebeRetornarNombreYApellido()
        {
            // Arrange
            var estudiante = new Estudiante
            {
                Nombre = "Juan",
                Apellido = "Pérez"
            };
            
            // Act
            var nombreCompleto = estudiante.ObtenerNombreCompleto();
            
            // Assert
            Assert.Equal("Juan Pérez", nombreCompleto);
        }
    }
}