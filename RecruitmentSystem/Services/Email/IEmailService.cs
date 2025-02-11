namespace RecruitmentSystem.Services.Email;
public interface IEmailService
{
    Task<bool> sendMails(List<string> to,string subject,string body);
}