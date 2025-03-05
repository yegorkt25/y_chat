using System.Text.Json.Serialization;

namespace Y_Chat.Responses;

public class RecaptchaResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("challenge_ts")]
    public string ChallengeTs { get; set; }

    [JsonPropertyName("hostname")]
    public string Hostname { get; set; }

    [JsonPropertyName("error-codes")]
    public IEnumerable<string> ErrorCodes { get; set; }
}