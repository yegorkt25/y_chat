using System.Text.Json;
using Y_Chat.Responses;

namespace Y_Chat.Services;

public class ValidateGoogleCaptchaService(HttpClient httpClient) : IValidateGoogleCaptchaService
{
    public async Task<RecaptchaResponse> ValidateCaptcha(string captchaToken, string site, string secret)
    {
        var verifyUrl = "https://www.google.com/recaptcha/api/siteverify";

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("secret", secret),
            new KeyValuePair<string, string>("response", captchaToken)
        });

        var response = await httpClient.PostAsync(verifyUrl, content);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var recaptchaResult = JsonSerializer.Deserialize<RecaptchaResponse>(jsonResponse);

        return recaptchaResult;
    }
}