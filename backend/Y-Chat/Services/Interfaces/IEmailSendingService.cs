namespace Y_Chat.Services;

public interface IEmailSendingService
{
    public Task SendConfirmationEmail(string userEmail, string confirmationLink);
}