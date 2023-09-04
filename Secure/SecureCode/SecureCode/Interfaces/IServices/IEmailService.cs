namespace SecureCode.Interfaces.IServices
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string receiver, string subject, string body);
    }
}
