using Y_Chat.Entities;

namespace Y_Chat.Responses;

public class AuthenticationResponse
{
    public JwtToken JwtToken { get; set; }
}