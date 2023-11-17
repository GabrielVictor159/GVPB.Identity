using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using ManagementServices.variables.Application.Interfaces;
using GVPB.Identity.Application;
namespace GVPB.Identity.Infraestructure;

public class EmailService : IEmailService
{
    private readonly IEnvVariableRepository envVariableRepository;

    public EmailService(IEnvVariableRepository envVariableRepository)
    {
        this.envVariableRepository = envVariableRepository;
    }

    public void SendEmail(string To, string Subject, string Image, string? Redirect = null)
    {
        string elasticEmailUsername = envVariableRepository.Get("EMAIL_USERNAME")!.Value; 
        string elasticEmailPassword = envVariableRepository.Get("EMAIL_PASSWORD")!.Value;
        string smtpServer = envVariableRepository.Get("SMTP_SERVER")!.Value;
        int smtpPort = int.Parse(envVariableRepository.Get("SMTP_PORT")!.Value);
        using (SmtpClient client = new SmtpClient(smtpServer))
        {
            client.Port = smtpPort;
            client.Credentials = new NetworkCredential(elasticEmailUsername, elasticEmailPassword);

            client.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.To.Add(To);
            mail.Subject = Subject;
            string htmlBody = $@"<!DOCTYPE html>
                                <html>
                                <body style='text-align: center;'>
                                    <a {(Redirect != null ? $"href={Redirect}":"")}>
                                        <img src='data:image/png;base64,{Image}' alt='Imagem Incorporada' style='display: block; margin: 0 auto;'>
                                    </a>
                                </body>
                                </html>";

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            mail.AlternateViews.Add(htmlView);
            client.Send(mail);
        }
    }
}
