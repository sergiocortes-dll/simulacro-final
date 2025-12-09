using MimeKit;
using MailKit.Net.Smtp;
using EduTrack.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EduTrack.Infrastructure.ExternalServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendWelcomeEmailAsync(string toEmail, string studentName, string documentNumber)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(
                    smtpSettings["SenderName"], 
                    smtpSettings["SenderEmail"]));
                message.To.Add(new MailboxAddress(studentName, toEmail));
                message.Subject = $"¡Bienvenido a EduTrack, {studentName}!";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                    <h1>¡Bienvenido a EduTrack!</h1>
                    <p>Estimado/a {studentName},</p>
                    <p>Tu registro ha sido exitoso. Aquí están tus detalles:</p>
                    <ul>
                        <li>Número de documento: {documentNumber}</li>
                        <li>Correo electrónico: {toEmail}</li>
                    </ul>
                    <p>Accede a tu portal aquí: https://portal.edutrack.com</p>
                    <br>
                    <p>¡Éxito en tus estudios!</p>
                    <p>Equipo EduTrack</p>"
                };

                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                
                await client.ConnectAsync(
                    smtpSettings["Host"], 
                    int.Parse(smtpSettings["Port"]), 
                    bool.Parse(smtpSettings["UseSsl"]));
                
                await client.AuthenticateAsync(
                    smtpSettings["Username"], 
                    smtpSettings["Password"]);
                
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation($"Email de bienvenida enviado a {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error enviando email a {toEmail}");
                throw;
            }
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string resetToken)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(
                    smtpSettings["SenderName"], 
                    smtpSettings["SenderEmail"]));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = "Recuperación de Contraseña - EduTrack";

                var resetLink = $"{smtpSettings["BaseUrl"]}/reset-password?token={resetToken}";
                
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                    <h1>Recuperación de Contraseña</h1>
                    <p>Has solicitado recuperar tu contraseña.</p>
                    <p>Haz clic en el siguiente enlace para restablecer tu contraseña:</p>
                    <p><a href='{resetLink}'>Restablecer Contraseña</a></p>
                    <p>Este enlace expirará en 24 horas.</p>
                    <br>
                    <p>Si no solicitaste este cambio, ignora este email.</p>"
                };

                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                
                await client.ConnectAsync(
                    smtpSettings["Host"], 
                    int.Parse(smtpSettings["Port"]), 
                    bool.Parse(smtpSettings["UseSsl"]));
                
                await client.AuthenticateAsync(
                    smtpSettings["Username"], 
                    smtpSettings["Password"]);
                
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation($"Email de recuperación enviado a {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error enviando email de recuperación a {toEmail}");
                throw;
            }
        }

        public async Task SendAcademicNotificationAsync(string toEmail, string subject, string messageBody)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(
                    smtpSettings["SenderName"], 
                    smtpSettings["SenderEmail"]));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = messageBody
                };

                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                
                await client.ConnectAsync(
                    smtpSettings["Host"], 
                    int.Parse(smtpSettings["Port"]), 
                    bool.Parse(smtpSettings["UseSsl"]));
                
                await client.AuthenticateAsync(
                    smtpSettings["Username"], 
                    smtpSettings["Password"]);
                
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation($"Notificación académica enviada a {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error enviando notificación académica a {toEmail}");
                throw;
            }
        }
    }
}