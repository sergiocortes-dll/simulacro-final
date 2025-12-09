using EduTrack.Domain.Exceptions;

namespace EduTrack.Domain.Entities;

public class Student : BaseEntity
{
    // Personal data
    public string DocumentNumber { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public DateTime BirthDate { get; private set; }
    public string Address { get; private set; } = string.Empty;
    public string? EmergencyContact  { get; private set; }
    public string? EmergencyPhone { get; private set; }
    
    // Academic Data
    public int AcademicProgramId { get; private set; }
    public virtual AcademicProgram AcademicProgram { get; private set; } = null!;
    public DateTime EnrollmentDate { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public int CurrentSemester { get; private set; }
    public string Modality { get; private set; } = string.Empty;
    public decimal? GPA { get; private set; }
    public int? TotalCredits { get; private set; }
    
    // Auth
    public string PasswordHash { get; private set; } = string.Empty;
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public bool IsActive { get; private set; }
    
    // Academic registers
    public virtual ICollection<AcademicRecord> AcademicRecords { get; private set; } = new List<AcademicRecord>;
    public virtual ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();
    
    // EF Core Constructor
    private Student()
    {
    }
    
    // Public Constructor for creation
    public Student(
        string documentNumber,
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        DateTime birthDate,
        string address,
        int academicProgramId,
        string modality = "Presencial")
    {
        SetDocumentNumber(documentNumber);
        SetFirstName(firstName);
        SetLastName(lastName);
        SetEmail(email);
        SetPhoneNumber(phoneNumber);
        SetBirthDate(birthDate);
        SetAddress(address);
        SetAcademicProgramId(academicProgramId);
        SetModality(modality);
        
        // Values by default
        Status = "Activo";
        CurrentSemester = 1;
        EnrollmentDate = DateTime.Now;
        IsActive = true;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
    
    // Business methods (setters with validation)
    public void SetDocumentNumber(string documentNumber)
    {
        if (string.IsNullOrWhiteSpace(documentNumber))
            throw new DomainException("El número de documento es obligatorio.");

        if (documentNumber.Length < 5 || documentNumber.Length > 20)
            throw new DomainException("El documento debe tener entre 5 y 20 caracteres.");

        DocumentNumber = documentNumber;
        UpdatedAt = DateTime.Now;
    }
    
    public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("El nombre es obligatorio");

            if (firstName.Length > 100)
                throw new DomainException("El nombre no puede exceder 100 caracteres");

            FirstName = firstName.Trim();
            UpdatedAt = DateTime.Now;
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("El apellido es obligatorio");

            if (lastName.Length > 100)
                throw new DomainException("El apellido no puede exceder 100 caracteres");

            LastName = lastName.Trim();
            UpdatedAt = DateTime.Now;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("El email es obligatorio");

            if (email.Length > 200)
                throw new DomainException("El email no puede exceder 200 caracteres");

            if (!IsValidEmail(email))
                throw new DomainException("Formato de email inválido");

            Email = email.ToLower().Trim();
            UpdatedAt = DateTime.Now;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException("El número de teléfono es obligatorio");

            if (phoneNumber.Length > 20)
                throw new DomainException("El teléfono no puede exceder 20 caracteres");

            PhoneNumber = phoneNumber.Trim();
            UpdatedAt = DateTime.Now;
        }

        public void SetBirthDate(DateTime birthDate)
        {
            if (birthDate > DateTime.Now.AddYears(-14))
                throw new DomainException("El estudiante debe tener al menos 14 años");

            if (birthDate < DateTime.Now.AddYears(-100))
                throw new DomainException("Fecha de nacimiento inválida");

            BirthDate = birthDate;
            UpdatedAt = DateTime.Now;
        }

        public void SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new DomainException("La dirección es obligatoria");

            if (address.Length > 200)
                throw new DomainException("La dirección no puede exceder 200 caracteres");

            Address = address.Trim();
            UpdatedAt = DateTime.Now;
        }

        public void SetAcademicProgramId(int academicProgramId)
        {
            if (academicProgramId <= 0)
                throw new DomainException("El programa académico es obligatorio");

            AcademicProgramId = academicProgramId;
            UpdatedAt = DateTime.Now;
        }

        public void SetModality(string modality)
        {
            var validModalities = new[] { "Presencial", "Virtual", "Híbrido" };
            
            if (!validModalities.Contains(modality))
                throw new DomainException($"Modalidad inválida. Válidas: {string.Join(", ", validModalities)}");

            Modality = modality;
            UpdatedAt = DateTime.Now;
        }
        
        public void SetStatus(string status)
        {
            var validStatuses = new[] { "Activo", "Inactivo", "Graduado", "Retirado", "Suspendido" };
            
            if (!validStatuses.Contains(status))
                throw new DomainException($"Estado inválido. Válidos: {string.Join(", ", validStatuses)}");

            Status = status;
            
            // Si se marca como inactivo o retirado
            if (status == "Inactivo" || status == "Retirado")
                IsActive = false;
            else
                IsActive = true;
                
            UpdatedAt = DateTime.Now;
        }

        public void SetCurrentSemester(int semester)
        {
            if (semester < 1)
                throw new DomainException("El semestre debe ser al menos 1");

            if (semester > 20) // Asumiendo máximo 20 semestres
                throw new DomainException("Semestre inválido");

            CurrentSemester = semester;
            UpdatedAt = DateTime.Now;
        }

        public void AdvanceSemester()
        {
            CurrentSemester++;
            UpdatedAt = DateTime.Now;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                throw new DomainException("La contraseña debe tener al menos 6 caracteres");

            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            UpdatedAt = DateTime.Now;
        }

        public bool VerifyPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        public void SetRefreshToken(string refreshToken, int expiryDays = 7)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = DateTime.Now.AddDays(expiryDays);
            UpdatedAt = DateTime.Now;
        }

        public void ClearRefreshToken()
        {
            RefreshToken = null;
            RefreshTokenExpiryTime = null;
            UpdatedAt = DateTime.Now;
        }

        public bool IsRefreshTokenValid(string refreshToken)
        {
            return RefreshToken == refreshToken && 
                   RefreshTokenExpiryTime.HasValue && 
                   RefreshTokenExpiryTime > DateTime.Now;
        }

        public void UpdateLastLogin()
        {
            LastLoginAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateGPA()
        {
            if (AcademicRecords == null || !AcademicRecords.Any())
            {
                GPA = null;
                return;
            }

            var approvedRecords = AcademicRecords
                .Where(r => r.Status == "Aprobado" && r.Grade.HasValue)
                .ToList();

            if (!approvedRecords.Any())
            {
                GPA = null;
                return;
            }

            GPA = approvedRecords.Average(r => r.Grade!.Value);
            UpdatedAt = DateTime.Now;
        }

        public void UpdateTotalCredits()
        {
            if (AcademicRecords == null || !AcademicRecords.Any())
            {
                TotalCredits = null;
                return;
            }

            var approvedRecords = AcademicRecords
                .Where(r => r.Status == "Aprobado")
                .ToList();

            TotalCredits = approvedRecords.Sum(r => r.Credits);
            UpdatedAt = DateTime.Now;
        }

        public void AddAcademicRecord(AcademicRecord record)
        {
            if (record == null)
                throw new DomainException("El registro académico no puede ser nulo");

            AcademicRecords.Add(record);
            UpdatedAt = DateTime.Now;
        }

        public string GetFullName() => $"{FirstName} {LastName}";

        public int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            
            if (BirthDate.Date > today.AddYears(-age))
                age--;
                
            return age;
        }

        public bool CanGraduate()
        {
            // Verificar si ha completado todos los créditos del programa
            if (AcademicProgram == null || AcademicProgram.TotalCredits == 0)
                return false;

            var totalApprovedCredits = AcademicRecords?
                .Where(r => r.Status == "Aprobado")
                .Sum(r => r.Credits) ?? 0;

            return totalApprovedCredits >= AcademicProgram.TotalCredits && 
                   CurrentSemester >= AcademicProgram.DurationInSemesters &&
                   GPA >= 3.0m; // GPA mínimo de 3.0
        }

        public void Graduate()
        {
            if (!CanGraduate())
                throw new DomainException("El estudiante no cumple los requisitos para graduarse");

            Status = "Graduado";
            IsActive = false;
            UpdatedAt = DateTime.Now;
        }

        // Método de validación privado
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
}