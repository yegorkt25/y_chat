using System.Text.RegularExpressions;

namespace Y_Chat.Services;

public static class PasswordValidationService
{
    public static (bool isValid, List<string>? Errors) Validate(string password)
    {
        List<string> errors = new();
        if (password.Length < 8)
        {
            errors.Add("Password must be at least 8 characters");
        }

        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            errors.Add("Password must contain at one letter.");

        }

        if (!Regex.IsMatch(password, @"\d"))
        {
            errors.Add("Password must contain at least one digit.");
        }

        return errors.Count == 0 ? (true, null) : (false, errors);
    }
}