using Y_Chat.Entities;

namespace Y_Chat.Services;

public interface IUserService
{
    public Task<User?> GetUserByToken(string token);
}