using EduTrack.Core.Dtos;
using EduTrack.Core.Entities;
using EduTrack.Core.Interfaces;
using EduTrack.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace EduTrack.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return null;

            var token = await GenerarTokenAsync(user);
            
            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                Nombre = user.Nombre,
                Expiracion = DateTime.Now.AddHours(24)
            };
        }

        public async Task<AuthResponseDto?> RegistroEstudianteAsync(RegistroDto dto)
        {
            // Verificar si el email ya existe
            var userExistente = await _userManager.FindByEmailAsync(dto.Email);
            if (userExistente != null)
                return null;

            // Verificar si el número de documento ya existe
            var estudianteExistente = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.NumeroDocumento == dto.NumeroDocumento);
            if (estudianteExistente != null)
                return null;

            // Crear usuario de Identity
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return null;

            // Asignar rol de estudiante
            await _userManager.AddToRoleAsync(user, "Estudiante");

            // Crear estudiante
            var estudiante = new Estudiante
            {
                NumeroDocumento = dto.NumeroDocumento,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                Telefono = "", // Puede ser actualizado después
                FechaNacimiento = DateTime.Now.AddYears(-18),
                FechaIngreso = DateTime.Now,
                Estado = "Activo",
                SemestreActual = 1,
                Observaciones = "",
                ProgramaAcademicoId = dto.ProgramaAcademicoId,
                TipoDocumentoId = dto.TipoDocumentoId,
                ModalidadId = dto.ModalidadId
            };

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            var token = await GenerarTokenAsync(user);
            
            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                Nombre = user.Nombre,
                Expiracion = DateTime.Now.AddHours(24)
            };
        }

        public async Task<EstudianteDto?> ObtenerEstudiantePorTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "clave-secreta-muy-larga-para-desarrollo");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;

                var estudiante = await _context.Estudiantes
                    .Include(e => e.ProgramaAcademico)
                    .Include(e => e.TipoDocumento)
                    .Include(e => e.Modalidad)
                    .FirstOrDefaultAsync(e => e.Email == email);

                if (estudiante == null) return null;

                return new EstudianteDto
                {
                    Id = estudiante.Id,
                    NumeroDocumento = estudiante.NumeroDocumento,
                    Nombre = estudiante.Nombre,
                    Apellido = estudiante.Apellido,
                    Email = estudiante.Email,
                    Telefono = estudiante.Telefono,
                    FechaNacimiento = estudiante.FechaNacimiento,
                    FechaIngreso = estudiante.FechaIngreso,
                    Estado = estudiante.Estado,
                    SemestreActual = estudiante.SemestreActual,
                    Observaciones = estudiante.Observaciones,
                    ProgramaAcademico = estudiante.ProgramaAcademico.Nombre,
                    TipoDocumento = estudiante.TipoDocumento.Nombre,
                    Modalidad = estudiante.Modalidad.Nombre
                };
            }
            catch
            {
                return null;
            }
        }

        private async Task<string> GenerarTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.Nombre)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "clave-secreta-muy-larga-para-desarrollo"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(24);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}