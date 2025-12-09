using System;

namespace EduTrack.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public string ErrorCode { get; }
        public int StatusCode { get; }

        public DomainException(string message) : base(message)
        {
            ErrorCode = "DOMAIN_ERROR";
            StatusCode = 400; // Bad Request by default
        }

        public DomainException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = 400;
        }

        public DomainException(string message, string errorCode, int statusCode) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = "DOMAIN_ERROR";
            StatusCode = 400;
        }
    }

    public class StudentDomainException : DomainException
    {
        public StudentDomainException(string message) : base(message, "STUDENT_ERROR") { }
        public StudentDomainException(string message, string errorCode) : base(message, errorCode) { }
    }

    public class AcademicProgramDomainException : DomainException
    {
        public AcademicProgramDomainException(string message) : base(message, "PROGRAM_ERROR") { }
    }

    public class EnrollmentDomainException : DomainException
    {
        public EnrollmentDomainException(string message) : base(message, "ENROLLMENT_ERROR") { }
    }

    public class AcademicRecordDomainException : DomainException
    {
        public AcademicRecordDomainException(string message) : base(message, "RECORD_ERROR") { }
    }

    public class ValidationDomainException : DomainException
    {
        public ValidationDomainException(string message) : base(message, "VALIDATION_ERROR", 422) { }
    }

    public class NotFoundDomainException : DomainException
    {
        public NotFoundDomainException(string entity, object key) 
            : base($"{entity} con ID {key} no fue encontrado.", "NOT_FOUND", 404) { }
    }

    public class DuplicateDomainException : DomainException
    {
        public DuplicateDomainException(string entity, string field, object value) 
            : base($"{entity} con {field} '{value}' ya existe.", "DUPLICATE", 409) { }
    }

    public class BusinessRuleViolationException : DomainException
    {
        public BusinessRuleViolationException(string rule, string message) 
            : base($"Violación de regla de negocio '{rule}': {message}", "BUSINESS_RULE", 422) { }
    }
}