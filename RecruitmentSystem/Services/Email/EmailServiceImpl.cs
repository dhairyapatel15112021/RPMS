using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using RecruitmentSystem.Data;

namespace RecruitmentSystem.Services.Email;

public class EmailServiceImpl : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailServiceImpl(ApplicationDbContext context, IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<bool> sendMails(List<string> recivers, string subject, string body)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Email:username"]));
            foreach (string to in recivers)
            {
                email.To.Add(MailboxAddress.Parse(to));
            }
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["Email:host"], 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration["Email:username"], _configuration["Email:password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception In Email Service");
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}