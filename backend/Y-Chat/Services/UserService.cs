using Microsoft.EntityFrameworkCore;
using Y_Chat.Entities;
using Y_Chat.Helpers;

namespace Y_Chat.Services;

public class UserService(JwtTokenHelper jwtTokenHelper, AppDbContext context) : IUserService
{
    public async Task<User?> GetUserByToken(string token)
    {
        var email = jwtTokenHelper.ValidateToken(token);

        if (email is null)
        {
            return null;
        }
        
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        
        return user;
    }
}