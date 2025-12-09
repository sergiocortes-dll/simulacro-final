-- Script de inicialización para EduTrackDB
-- Este script se ejecuta automáticamente cuando se crea el contenedor MySQL

USE EduTrackDB;

-- Crear tabla de auditoría
CREATE TABLE IF NOT EXISTS Auditoria (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Tabla VARCHAR(100) NOT NULL,
    Accion VARCHAR(50) NOT NULL,
    Usuario VARCHAR(100) NOT NULL,
    FechaAccion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DatosAnteriores JSON,
    DatosNuevos JSON,
    DireccionIP VARCHAR(45)
);

-- Crear vista de estadísticas
CREATE VIEW EstadisticasEstudiantes AS
SELECT 
    (SELECT COUNT(*) FROM Estudiantes) as TotalEstudiantes,
    (SELECT COUNT(*) FROM Estudiantes WHERE Estado = 'Activo') as EstudiantesActivos,
    (SELECT COUNT(*) FROM Estudiantes WHERE ModalidadId = 1) as EstudiantesPresenciales,
    (SELECT COUNT(*) FROM Estudiantes WHERE ModalidadId = 2) as EstudiantesVirtuales,
    (SELECT COUNT(*) FROM Estudiantes WHERE ModalidadId = 3) as EstudiantesMixtos;

-- Crear procedimiento almacenado para reportes
DELIMITER //
CREATE PROCEDURE ObtenerReporteEstudiantes(
    IN fechaInicio DATE,
    IN fechaFin DATE,
    IN programaId INT
)
BEGIN
    SELECT 
        e.Id,
        e.NumeroDocumento,
        e.Nombre,
        e.Apellido,
        e.Email,
        e.Telefono,
        e.FechaNacimiento,
        e.FechaIngreso,
        e.Estado,
        e.SemestreActual,
        e.Observaciones,
        pa.Nombre as ProgramaAcademico,
        td.Nombre as TipoDocumento,
        m.Nombre as Modalidad
    FROM Estudiantes e
    INNER JOIN ProgramasAcademicos pa ON e.ProgramaAcademicoId = pa.Id
    INNER JOIN TiposDocumento td ON e.TipoDocumentoId = td.Id
    INNER JOIN Modalidades m ON e.ModalidadId = m.Id
    WHERE e.FechaIngreso BETWEEN fechaInicio AND fechaFin
    AND (programaId = 0 OR e.ProgramaAcademicoId = programaId)
    ORDER BY e.FechaIngreso DESC;
END//
DELIMITER ;

-- Insertar datos de ejemplo para pruebas
INSERT INTO Estudiantes (
    NumeroDocumento, Nombre, Apellido, Email, Telefono, 
    FechaNacimiento, FechaIngreso, Estado, SemestreActual, 
    Observaciones, ProgramaAcademicoId, TipoDocumentoId, ModalidadId
) VALUES 
('123456789', 'Juan', 'Pérez', 'juan.perez@email.com', '3001234567', 
 '2000-05-15', '2024-01-15', 'Activo', 3, 'Excelente rendimiento académico', 1, 1, 1),
('987654321', 'María', 'Gómez', 'maria.gomez@email.com', '3007654321', 
 '1999-08-22', '2024-01-20', 'Activo', 2, 'Requiere apoyo académico', 2, 1, 2),
('456789123', 'Carlos', 'Rodríguez', 'carlos.rodriguez@email.com', '3004567891', 
 '2001-03-10', '2024-02-01', 'Activo', 1, 'Nuevo ingreso', 3, 1, 1),
('321654987', 'Ana', 'Martínez', 'ana.martinez@email.com', '3003216549', 
 '1998-12-05', '2024-02-10', 'Activo', 4, 'Participación en proyectos de investigación', 4, 2, 3);

-- Mensaje de confirmación
SELECT 'Base de datos EduTrack inicializada correctamente' AS Mensaje;