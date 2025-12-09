using EduTrack.Core.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace EduTrack.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> EnviarEmailConfirmacionRegistroAsync(string email, string nombre, string numeroDocumento)
        {
            var asunto = "¡Bienvenido a EduTrack! - Confirmación de Registro";
            var mensaje = $@"
                <html>
                <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f8f9fa;'>
                        <div style='background-color: #007bff; color: white; padding: 20px; text-align: center; border-radius: 5px;'>
                            <h1>¡Bienvenido a EduTrack!</h1>
                        </div>
                        <div style='background-color: white; padding: 30px; margin-top: 20px; border-radius: 5px; box-shadow: 0 2px 5px rgba(0,0,0,0.1);'>
                            <h2>Hola {nombre},</h2>
                            <p>¡Tu registro ha sido exitoso! Ahora formas parte de la comunidad EduTrack.</p>
                            
                            <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                                <h3>Datos de tu registro:</h3>
                                <p><strong>Número de Documento:</strong> {numeroDocumento}</p>
                                <p><strong>Email:</strong> {email}</p>
                                <p><strong>Fecha de Registro:</strong> {DateTime.Now:yyyy-MM-dd HH:mm}</p>
                            </div>
                            
                            <p>Ya puedes acceder al portal del estudiante usando tus credenciales.</p>
                            
                            <div style='text-align: center; margin-top: 30px;'>
                                <p style='color: #6c757d; font-size: 12px;'>
                                    Este es un correo automático, por favor no respondas a este mensaje.
                                </p>
                            </div>
                        </div>
                    </div>
                </body>
                </html>
            ";

            return await EnviarEmailAsync(email, asunto, mensaje);
        }

        public async Task<bool> EnviarEmailAsync(string destinatario, string asunto, string mensaje)
        {
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings");
                var smtpServer = emailSettings["SmtpServer"] ?? "smtp.gmail.com";
                var smtpPort = int.Parse(emailSettings["SmtpPort"] ?? "587");
                var smtpUser = emailSettings["SmtpUser"] ?? "";
                var smtpPass = emailSettings["SmtpPass"] ?? "";

                if (string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPass))
                {
                    // En desarrollo, simular el envío exitoso
                    Console.WriteLine($"[SIMULACIÓN EMAIL] Para: {destinatario}, Asunto: {asunto}");
                    return true;
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("EduTrack", smtpUser));
                message.To.Add(new MailboxAddress("", destinatario));
                message.Subject = asunto;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = mensaje
                };
                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpUser, smtpPass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando email: {ex.Message}");
                return false;
            }
        }
    }
}