using Y_Chat.Responses;

namespace Y_Chat.Services;

public interface IValidateGoogleCaptchaService
{
    public Task<RecaptchaResponse> ValidateCaptcha(string captchaToken, string site, string secret);
}