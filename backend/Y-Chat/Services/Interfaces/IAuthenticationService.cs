using Y_Chat.DTOs;
using Y_Chat.Entities;
using Y_Chat.Responses;

namespace Y_Chat.Services;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> Login(LoginDTO dto);
    Task<AuthenticationResponse> Register(RegisterDTO dto);
    Task<EmailConfirmationResponse> ConfirmEmail(string token);
    Task<AddProfileDetailsResponse> AddProfileDetails(AddProfileDetailsDTO dto, string token); 
}