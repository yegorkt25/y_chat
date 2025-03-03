namespace Y_Chat.Services;

public interface IEmailTokenService
{
    public string GenerateToken(string email);
}