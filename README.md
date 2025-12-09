# EduTrack - Sistema de Gestión Académica

## 🎯 Descripción

EduTrack es un sistema de gestión académica completo desarrollado con .NET 9, Entity Framework Core y MySQL. Este proyecto está diseñado específicamente para estudiantes que están aprendiendo C# y desean comprender cómo construir aplicaciones web empresariales usando las mejores prácticas de la industria.

## 🛠️ Tecnologías Utilizadas

### Backend
- **.NET 9** - Framework principal
- **Entity Framework Core** - ORM para acceso a datos
- **MySQL** - Base de datos relacional
- **ASP.NET Core Web API** - API REST
- **ASP.NET Core MVC** - Aplicación web para administradores
- **Dependency Injection** - Inyección de dependencias

### Frontend
- **Bootstrap 5** - Framework CSS
- **jQuery** - Librería JavaScript
- **DataTables** - Tablas interactivas
- **Bootstrap Icons** - Iconos

### Herramientas de Desarrollo
- **Docker** - Contenerización
- **Docker Compose** - Orquestación de contenedores
- **Swagger/OpenAPI** - Documentación de API
- **xUnit** - Framework de pruebas

## 🏗️ Arquitectura

El proyecto implementa una arquitectura en capas basada en Domain-Driven Design (DDD) simplificado:

```
┌─────────────────────────────────────┐
│         Capa de Presentación        │
│  ┌─────────────┐  ┌──────────────┐  │
│  │   WebAPI    │  │    Web MVC   │  │
│  │  (Estudiantes) │  (Administradores) │
│  └─────────────┘  └──────────────┘  │
├─────────────────────────────────────┤
│        Capa de Aplicación           │
│         ┌─────────────┐             │
│         │  Services   │             │
│         └─────────────┘             │
├─────────────────────────────────────┤
│          Capa de Dominio            │
│  ┌─────────────┐  ┌──────────────┐  │
│  │  Entities   │  │ Repositories │  │
│  └─────────────┘  └──────────────┘  │
├─────────────────────────────────────┤
│       Capa de Infraestructura       │
│  ┌─────────────┐  ┌──────────────┐  │
│  │    Data     │  │Repositories  │  │
│  │ (DbContext) │  │ (Impl.)      │  │
│  └─────────────┘  └──────────────┘  │
└─────────────────────────────────────┘
```

## 📁 Estructura del Proyecto

```
EduTrack/
├── src/
│   ├── Domain/                    # Capa de dominio
│   │   ├── Entities/              # Entidades del negocio
│   │   └── Repositories/          # Interfaces de repositorios
│   ├── Application/               # Capa de aplicación
│   │   └── Services/              # Servicios de aplicación
│   ├── Infrastructure/            # Capa de infraestructura
│   │   ├── Data/                  # DbContext y configuraciones
│   │   └── Repositories/          # Implementaciones de repositorios
│   ├── WebAPI/                    # API REST
│   │   ├── Controllers/           # Controladores API
│   │   └── Program.cs             # Punto de entrada
│   └── EduTrack.Web/              # Aplicación Web MVC
│       ├── Controllers/           # Controladores MVC
│       ├── Views/                 # Vistas Razor
│       └── Program.cs             # Punto de entrada
├── tests/
│   ├── Domain.Tests/              # Pruebas del dominio
│   └── Application.Tests/         # Pruebas de aplicación
├── docker-compose.yml             # Orquestación con Docker
├── Dockerfile                     # Imagen Docker
├── Documentacion.html             # Documentación completa
├── README.md                      # Este archivo
└── EduTrack.sln                   # Solución Visual Studio
```

## 🚀 Cómo Empezar

### Requisitos Previos
- .NET 9 SDK
- MySQL Server 8.0 o superior
- Docker Desktop (opcional)
- Visual Studio 2022 o Visual Studio Code

### Opción 1: Ejecución Local

1. **Clonar el repositorio**
```bash
git clone [URL_DEL_REPOSITORIO]
cd EduTrack
```

2. **Configurar la conexión a MySQL**
Editar `appsettings.json` en WebAPI y Web:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=edutrack_db;User=root;Password=tu_password;"
  }
}
```

3. **Restaurar paquetes NuGet**
```bash
dotnet restore
```

4. **Ejecutar la aplicación**
```bash
# API REST
dotnet run --project src/WebAPI/EduTrack.WebAPI.csproj

# Aplicación Web (en otra terminal)
dotnet run --project src/EduTrack.Web/EduTrack.Web.csproj
```

### Opción 2: Ejecución con Docker

```bash
# Construir y ejecutar con Docker Compose
docker-compose up --build

# La aplicación estará disponible en:
# - API REST: http://localhost:8080
# - MySQL: localhost:3306
# - Swagger UI: http://localhost:8080/swagger
```

## 📱 Uso de la Aplicación

### Para Administradores
1. Acceder a la aplicación web en `http://localhost:5000`
2. Navegar por el dashboard para ver métricas
3. Gestionar estudiantes (CRUD completo)
4. Ver reportes y estadísticas
5. Usar la consulta con IA para obtener respuestas rápidas

### Para Estudiantes (API REST)
1. Consultar programas disponibles: `GET /api/programas`
2. Registrarse como nuevo estudiante: `POST /api/estudiantes/register`
3. Consultar información personal (con autenticación)

### Pruebas con Swagger
La API incluye documentación Swagger que puedes acceder en:
```
http://localhost:5001/swagger
```

## 🔌 API REST Endpoints

### Endpoints Públicos
- `GET /api/programas` - Obtener todos los programas académicos
- `POST /api/estudiantes/register` - Registro de nuevos estudiantes

### Endpoints de Administrador
- `GET /api/estudiantes` - Obtener todos los estudiantes
- `GET /api/estudiantes/{id}` - Obtener un estudiante específico
- `POST /api/estudiantes` - Crear un nuevo estudiante
- `PUT /api/estudiantes/{id}` - Actualizar un estudiante
- `DELETE /api/estudiantes/{id}` - Eliminar un estudiante
- `GET /api/dashboard/metricas` - Obtener métricas del dashboard

## 🧪 Pruebas

Ejecutar las pruebas unitarias:
```bash
dotnet test
```

Las pruebas incluyen:
- Validaciones de entidades del dominio
- Pruebas de servicios de aplicación
- Pruebas de repositorios

## 🎓 Conceptos Clave para Aprender

### Patrones de Diseño
- **Repository Pattern**: Abstrae el acceso a datos
- **Service Layer**: Lógica de aplicación centralizada
- **Dependency Injection**: Inversión de control
- **DTOs**: Transferencia de datos

### Principios SOLID
- **Single Responsibility**: Una clase, una responsabilidad
- **Open/Closed**: Abierto a extensión, cerrado a modificación
- **Liskov Substitution**: Sustitución de objetos
- **Interface Segregation**: Interfaces específicas
- **Dependency Inversion**: Depender de abstracciones

### Mejores Prácticas Implementadas
- Separación de capas
- Arquitectura DDD simplificada
- Inyección de dependencias
- Configuración por ambientes
- Validación de datos
- Manejo de errores
- Logging estructurado

## 🚀 Próximos Pasos y Mejoras

### Funcionalidades Pendientes
- [ ] Autenticación JWT
- [ ] Importación desde Excel
- [ ] Generación de reportes PDF
- [ ] Integración con IA real (OpenAI, Gemini)
- [ ] Envío de notificaciones por email
- [ ] Implementación de caché con Redis
- [ ] Sistema de logging con Serilog
- [ ] Health checks y métricas

### Mejoras de Código
- [ ] Optimización de operaciones asíncronas
- [ ] Implementación de FluentValidation
- [ ] Uso de AutoMapper
- [ ] Separación CQRS
- [ ] Domain Events
- [ ] Aumento de cobertura de pruebas

## 📚 Recursos Adicionales

### Documentación Oficial
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [C# Guide](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Docker Documentation](https://docs.docker.com/)

### Tutoriales y Cursos
- [.NET Learning Path](https://dotnet.microsoft.com/en-us/learn/aspnet)
- [Pluralsight .NET Path](https://www.pluralsight.com/paths/dotnet-core)
- [IAmTimCorey YouTube](https://www.youtube.com/c/IAmTimCorey)

## 🤝 Contribuir

¡Las contribuciones son bienvenidas! Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está licenciado bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

## 👨‍💻 Autor

**EduTrack Team**

- Proyecto educativo para aprendizaje de C# y .NET
- Diseñado para estudiantes con 2 meses de experiencia en C#
- Enfoque en prácticas reales y código limpio

## 🙏 Agradecimientos

- A la comunidad .NET por sus contribuciones y recursos
- A Microsoft por el excelente framework y documentación
- A todos los educadores que comparten su conocimiento

---

**¡Recuerda!** 📚

> "La mejor manera de aprender es practicando. No tengas miedo de experimentar, romper cosas y arreglarlas. ¡Cada error es una oportunidad de aprendizaje!"

---

<div align="center">
    <h3>⭐ Si este proyecto te fue útil, ¡dale una estrella! ⭐</h3>
</div>