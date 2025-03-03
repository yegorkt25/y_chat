using MailKit.Net.Smtp;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using MimeKit;

namespace Y_Chat.Services;

public class EmailSendingService : IEmailSendingService
{
    private readonly SmtpClient smtpClient;
    private readonly string fromAddress;

    public EmailSendingService(IConfiguration configuration)
    {
        smtpClient = new SmtpClient();
        smtpClient.Connect(configuration["MailSettings:Host"], int.Parse(configuration["MailSettings:Port"]), false);
        smtpClient.Authenticate(configuration["MailSettings:Username"], configuration["MailSettings:Password"]);
        
        fromAddress = configuration["MailSettings:FromAddress"];
    }

    public async Task SendConfirmationEmail(string userEmail, string confirmationLink)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Your App", fromAddress));
        message.To.Add(new MailboxAddress("", userEmail));
        message.Subject = "Email Confirmation";
        
        var body = new TextPart("plain")
        {
            Text = $"Please confirm your email by clicking the following link: {confirmationLink}"
        };
        
        message.Body = body;
        await smtpClient.SendAsync(message);
        await smtpClient.DisconnectAsync(true);
        smtpClient.Dispose();
    }
}