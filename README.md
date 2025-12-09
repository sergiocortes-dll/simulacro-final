# EduTrack S.A.S. - Sistema de Gestión Académica

Sistema completo de gestión de estudiantes desarrollado con .NET 9, Entity Framework, MySQL y Docker.

## 🚀 Características Principales

### Aplicación Web (Administradores)
- ✅ **CRUD Completo de Estudiantes**
- ✅ **Importación desde Excel**
- ✅ **Generación de PDFs (Hojas Académicas)**
- ✅ **Dashboard con IA**
- ✅ **Autenticación con ASP.NET Core Identity**

### API REST (Estudiantes)
- ✅ **Autoregistro de Estudiantes**
- ✅ **Login con JWT**
- ✅ **Consulta de información personal**
- ✅ **Listado de Programas Académicos**

### Funcionalidades Avanzadas
- ✅ **Inteligencia Artificial** - Consultas en lenguaje natural
- ✅ **Envío de Emails** - Confirmación de registro
- ✅ **Docker Compose** - Ejecución completa con un comando
- ✅ **Arquitectura DDD** - Simple pero completa

## 🛠️ Tecnologías Utilizadas

- **.NET 9** - Framework principal
- **Entity Framework Core 9** - ORM
- **MySQL** - Base de datos
- **ASP.NET Core Identity** - Autenticación
- **JWT** - Tokens de autenticación
- **Docker & Docker Compose** - Containerización
- **QuestPDF** - Generación de PDFs
- **EPPlus** - Procesamiento de Excel
- **MailKit** - Envío de emails
- **Chart.js** - Gráficos en el dashboard

## 📁 Estructura del Proyecto

```
EduTrack/
├── EduTrack.Core/              # Capa de dominio (DDD)
│   ├── Entities/                # Entidades principales
│   ├── Dtos/                   # Data Transfer Objects
│   └── Interfaces/             # Contratos de servicios
├── EduTrack.Infrastructure/    # Capa de infraestructura
│   ├── Data/                   # DbContext y configuraciones
│   └── Services/               # Implementaciones de servicios
├── EduTrack.Web/              # Aplicación Web MVC
│   ├── Controllers/           # Controladores MVC
│   └── Views/                 # Vistas Razor
├── EduTrack.API/              # API REST
│   └── Controllers/           # Controladores API
├── docker-compose.yml         # Orquestación Docker
├── Dockerfile-api             # Dockerfile para API
├── Dockerfile-web             # Dockerfile para Web
└── README.md                  # Este archivo
```

## 🚦 Cómo Iniciar el Proyecto

### Opción 1: Docker Compose (Recomendado)

1. **Clonar el repositorio**
```bash
git clone <url-del-repositorio>
cd EduTrack
```

2. **Ejecutar con Docker Compose**
```bash
docker-compose up -d
```

3. **Acceder a las aplicaciones**
- **Aplicación Web**: http://localhost:5001
- **API REST**: http://localhost:5000
- **Swagger UI**: http://localhost:5000 (raíz)

### Opción 2: Ejecución Local

1. **Requisitos previos**
- .NET 9 SDK
- MySQL Server
- Visual Studio 2022 o VS Code

2. **Configurar la base de datos**
```sql
CREATE DATABASE EduTrackDB;
```

3. **Configurar la cadena de conexión**
En `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EduTrackDB;User=root;Password=tu-password;"
  }
}
```

4. **Aplicar migraciones**
```bash
dotnet ef database update --project EduTrack.Infrastructure --startup-project EduTrack.Web
dotnet ef database update --project EduTrack.Infrastructure --startup-project EduTrack.API
```

5. **Ejecutar los proyectos**
```bash
# API REST
dotnet run --project EduTrack.API

# Aplicación Web (en otra terminal)
dotnet run --project EduTrack.Web
```

## 👤 Credenciales de Acceso

### Administrador
- **Email**: `admin@edutrack.com`
- **Contraseña**: `Admin123!`

### Registro de Estudiantes
Los estudiantes pueden registrarse directamente en:
- **API REST**: `POST /api/auth/registro`
- **Aplicación Web**: Formulario de registro

## 📚 Uso de la API REST

### Endpoints Públicos (Sin Autenticación)

#### 1. Listar Programas Académicos
```http
GET /api/programas
```

#### 2. Autoregistro de Estudiante
```http
POST /api/auth/registro
Content-Type: application/json

{
  "numeroDocumento": "123456789",
  "nombre": "Juan",
  "apellido": "Pérez",
  "email": "juan.perez@email.com",
  "password": "Password123!",
  "programaAcademicoId": 1,
  "tipoDocumentoId": 1,
  "modalidadId": 1
}
```

#### 3. Login de Estudiante
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "juan.perez@email.com",
  "password": "Password123!"
}
```

### Endpoints Protegidos (Requieren JWT)

#### Consultar Información Personal
```http
GET /api/auth/perfil
Authorization: Bearer <token-jwt>
```

## 🎨 Dashboard con IA

El dashboard incluye un asistente de IA que puede responder preguntas como:

- "¿Cuántos estudiantes hay en total?"
- "¿Cuántos estudiantes están activos?"
- "¿Cuántos estudiantes están en modalidad virtual?"
- "¿Cuántos estudiantes hay por programa?"
- "¿Cuántos estudiantes hay por semestre?"

## 📊 Importación de Excel

### Formato del Archivo
El archivo Excel debe tener las siguientes columnas:

| Columna | Descripción | Ejemplo |
|---------|-------------|---------|
| 1 | Número de Documento | 123456789 |
| 2 | Tipo de Documento | CC |
| 3 | Nombre | Juan |
| 4 | Apellido | Pérez |
| 5 | Email | juan@email.com |
| 6 | Teléfono | 3001234567 |
| 7 | Fecha de Nacimiento | 2000-01-15 |
| 8 | Código de Programa | ING-SIS |
| 9 | Código de Modalidad | PRESENCIAL |

### Códigos Válidos

**Tipos de Documento:**
- CC: Cédula de Ciudadanía
- TI: Tarjeta de Identidad
- CE: Cédula de Extranjería
- PAS: Pasaporte

**Modalidades:**
- PRESENCIAL
- VIRTUAL
- MIXTA

**Programas Académicos:**
- ING-SIS: Ingeniería de Sistemas
- ADM-EMP: Administración de Empresas
- CON-ADMON: Contaduría Pública
- PSI-CLIN: Psicología Clínica

## 🔧 Configuración de Email

Para habilitar el envío de emails reales, configura en `appsettings.json`:

```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SmtpUser": "tu-email@gmail.com",
    "SmtpPass": "tu-contraseña-o-app-password"
  }
}
```

## 🧪 Pruebas

### Probar la API con Swagger
1. Acceder a http://localhost:5000
2. Usar la documentación interactiva de Swagger
3. Probar los endpoints directamente desde la UI

### Probar la Aplicación Web
1. Acceder a http://localhost:5001
2. Iniciar sesión con las credenciales de administrador
3. Navegar por las diferentes secciones

## 🐛 Solución de Problemas

### Error de conexión a MySQL
```bash
# Verificar que el contenedor esté ejecutándose
docker ps

# Ver logs del contenedor MySQL
docker logs edutrack-mysql

# Reiniciar el contenedor
docker-compose restart mysql
```

### Error de migraciones
```bash
# Forzar actualización de la base de datos
dotnet ef database drop --force --project EduTrack.Infrastructure --startup-project EduTrack.Web
dotnet ef database update --project EduTrack.Infrastructure --startup-project EduTrack.Web
```

### Puerto ya en uso
```bash
# Verificar qué proceso está usando el puerto
netstat -an | grep 5000

# Cambiar puertos en docker-compose.yml
ports:
  - "5002:80"  # Nuevo puerto para API
  - "5003:80"  # Nuevo puerto para Web
```

## 📈 Próximas Mejoras

- [ ] Integración con servicios de IA reales (OpenAI, Gemini)
- [ ] Sistema de notificaciones push
- [ ] Aplicación móvil
- [ ] Reportes avanzados con gráficos
- [ ] Integración con sistemas de pago
- [ ] Chat en tiempo real

## 🤝 Contribuir

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## 📄 Licencia

Este proyecto está licenciado bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para detalles.

## 👨‍💻 Autor

**EduTrack S.A.S.** - Equipo de Desarrollo

- LinkedIn: [EduTrack S.A.S.](https://linkedin.com/company/edutrack)
- Email: desarrollo@edutrack.com

---

**¡Gracias por usar EduTrack! 🎓**