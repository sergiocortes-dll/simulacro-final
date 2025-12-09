using EduTrack.Application.Common.DTOs;

namespace EduTrack.Application.Common.Interfaces;

public interface IEmailService
{
    Task<EmailResultDto> SendEmailAsync(EmailMessageDto emailMessage);
    Task<EmailResultDto> SendWelcomeEmailAsync(StudentWelcomeEmailDto welcomeEmail);
    Task<EmailResultDto> SendPasswordResetEmailAsync(string email, string resetToken, string userId);
    Task<EmailResultDto> SendAcademicNotificationAsync(int studentId, string notificationType, object data);
}