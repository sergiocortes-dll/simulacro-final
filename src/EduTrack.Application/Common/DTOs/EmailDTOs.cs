namespace EduTrack.Application.Common.DTOs
{
    public class EmailMessageDto
    {
        public string To { get; set; } = string.Empty;
        public string? ToName { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool IsHtml { get; set; } = true;
        public List<EmailAttachmentDto>? Attachments { get; set; }
        public Dictionary<string, string>? TemplateVariables { get; set; }
        public string? TemplateId { get; set; }
    }

    public class EmailAttachmentDto
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] Content { get; set; } = Array.Empty<byte>();
        public string ContentType { get; set; } = string.Empty;
    }

    public class StudentWelcomeEmailDto
    {
        public string StudentName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AcademicProgram { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public string StudentPortalUrl { get; set; } = string.Empty;
        public string SupportEmail { get; set; } = string.Empty;
        public string CampusAddress { get; set; } = string.Empty;
        public string ImportantDates { get; set; } = string.Empty;
    }

    public class EmailResultDto
    {
        public bool Success { get; set; }
        public string MessageId { get; set; } = string.Empty;
        public DateTime SentDate { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}