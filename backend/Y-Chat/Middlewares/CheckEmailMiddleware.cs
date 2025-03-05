using Newtonsoft.Json;
using Y_Chat.Helpers;
using Y_Chat.Services;

namespace Y_Chat.Middlewares;

public class CheckEmailMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            await _next(context);
            return;        
        }
        
        var user = await userService.GetUserByToken(token);

        if (user == null)
        {
            await ReturnUnauthorizedResponse(context, "Token is invalid");
        }

        if (!user.EmailConfirmed)
        {
            await ReturnUnauthorizedResponse(context, "Email is not confirmed");
        }
        
        context.Items["User"] = user;
        await _next(context);
    }
    
    private async Task ReturnUnauthorizedResponse(HttpContext context, string message)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";
        var response = new { message = message };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}