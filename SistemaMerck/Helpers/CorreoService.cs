using SistemaMerck.Helpers.Interface;
using System.Net.Mail;
using System.Net;

namespace SistemaMerck.Helpers
{
    public class CorreoService : ICorreoService
    {
        private readonly IConfiguration _configuration;
        public CorreoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo)
        {
            var smtpHost = _configuration["CorreoElectronico:SmtpHost"];
            var smtpPort = int.Parse(_configuration["CorreoElectronico:SmtpPort"]);
            var smtpUsername = _configuration["CorreoElectronico:SmtpUsername"];
            var smtpPassword = _configuration["CorreoElectronico:SmtpPassword"];
            var senderEmail = _configuration["CorreoElectronico:SenderEmail"];
            var senderName = _configuration["CorreoElectronico:SenderName"];

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
            }
        }
    }
}
