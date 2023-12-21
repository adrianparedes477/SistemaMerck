using SistemaMerck.Helpers.Interface;
using System.Net.Mail;
using System.Net;
using RestSharp.Authenticators;
using RestSharp;

namespace SistemaMerck.Helpers
{
    public class CorreoService : ICorreoService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CorreoService> _logger;
        public CorreoService(IConfiguration configuration, ILogger<CorreoService> logger, IHostEnvironment env)
        {
            _configuration = configuration;
            _logger = logger;

        }

        public async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo)
        {
            var smtpHost = _configuration["CorreoElectronico:SmtpHost"];
            var smtpPort = int.Parse(_configuration["CorreoElectronico:SmtpPort"]);
            var smtpUsername = _configuration["CorreoElectronico:SmtpUsername"];
            var smtpPassword = _configuration["CorreoElectronico:SmtpPassword"];
            var senderEmail = _configuration["CorreoElectronico:SenderEmail"];
            var senderName = _configuration["CorreoElectronico:SenderName"];

            

            try
            {
                using (var client = new SmtpClient(smtpHost, smtpPort))
                {
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(senderEmail, senderName),
                        Subject = asunto,
                        Body = cuerpo,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(new MailAddress(destinatario));

                    await client.SendMailAsync(mailMessage);

                    _logger.LogInformation("Correo enviado correctamente.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar el correo: {ex.Message}");
                throw;
            }
        }
    }


}
