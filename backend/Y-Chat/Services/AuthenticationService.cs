using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Y_Chat.DTOs;
using Y_Chat.Entities;
using Y_Chat.Helpers;
using Y_Chat.Responses;

namespace Y_Chat.Services;

public class AuthenticationService(
    JwtTokenHelper jwtTokenHelper,
    AppDbContext context,
    IEmailTokenService emailTokenService,
    IEmailSendingService emailSendingService,
    IConfiguration configuration) : IAuthenticationService
{
    public async Task<AuthenticationResponse> Login(LoginDTO dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Email == dto.Email);
        if (user is null)
        {
            //TODO Change exception handling
            throw new Exception("User wasn't found");
        }

        if (!PasswordVerificationService.VerifyPassword(password: dto.Password, hashPassword: user.PasswordHash))
        {
            //TODO Change exception handling
            throw new Exception("Wrong password");
        }

        if (!user.EmailConfirmed)
        {
            //TODO Change exception handling
            throw new Exception("Email is not confirmed");
        }
        Console.WriteLine(user.ProfileDetailsAdded);
        if (!user.ProfileDetailsAdded)
        {
            //TODO Change exception handling
            throw new Exception("Add profile details please");
        }

        var token = new JwtToken { Token = jwtTokenHelper.GenerateToken(user.Email) };
        return new AuthenticationResponse { JwtToken = token };
    }

    public async Task<AuthenticationResponse> Register(RegisterDTO dto)
    {
        var passwordValid = PasswordValidationService.Validate(dto.Password);
        var emailValid = EmailValidationService.IsValidEmail(dto.Email);

        if (!emailValid)
        {
            //TODO Change exception handling
            throw new Exception("Email is not valid");
        }

        if (!passwordValid.isValid)
        {
            //TODO Change exception handling
            throw new Exception("Password is not valid");
        }

        var user = new User
            { Email = dto.Email, PasswordHash = PasswordVerificationService.HashPassword(dto.Password) };
        await context.Users.AddAsync(user);

        var confirmationToken = emailTokenService.GenerateToken(user.Email);

        var confirmationUrl = $"http://localhost:14650/api/auth/confirm?token={confirmationToken}";

        await emailSendingService.SendConfirmationEmail(user.Email, confirmationUrl);

        await context.SaveChangesAsync();

        var token = new JwtToken { Token = jwtTokenHelper.GenerateToken(user.Email) };
        return new AuthenticationResponse { JwtToken = token };
    }

    public async Task<EmailConfirmationResponse> ConfirmEmail(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            //TODO Change exception handling
            throw new Exception("Invalid token.");
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:SecretKey"]);
        
        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out var validatedToken);

            var email = principal.Identity.Name;

            var user = await context.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.EmailConfirmed = true;
            await context.SaveChangesAsync();

            return new EmailConfirmationResponse { Message = "Email successfully confirmed" };
        }
        catch (Exception ex)
        {
            //TODO Change exception handling
            throw new Exception(ex.Message);
        }
    }

    public async Task<AddProfileDetailsResponse> AddProfileDetails(AddProfileDetailsDTO dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Id == dto.Id);
        
        if (user is null)
        {
            //TODO Change exception handling
            throw new Exception("User was not found");
        }
        
        if (user.ProfileDetailsAdded)
        {
            //TODO Change exception handling
            throw new Exception("Profile details already added");
        }
        
        var avatar = await ImageValidationService.ValidateImage(dto.AvatarURL);
        
        if (avatar is null)
        {
            //TODO Change exception handling
            throw new Exception("Size is not valid or url is invalid");
        }
        
        user.Username = dto.Username;
        user.AvatarUrl = avatar;
        user.ProfileDetailsAdded = true;
        
        await context.SaveChangesAsync();
        
        return new AddProfileDetailsResponse { Message = "Profile details added" };
    }
}