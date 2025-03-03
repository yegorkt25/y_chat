namespace Y_Chat.Services;
using BCrypt.Net;

public static class PasswordVerificationService
{
    public static string HashPassword(string password)
    {
        return BCrypt.EnhancedHashPassword(password);
    }

    public static bool VerifyPassword(string password, string hashPassword)
    {
        return BCrypt.EnhancedVerify(password, hashPassword);
    }
}