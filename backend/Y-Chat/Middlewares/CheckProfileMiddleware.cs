using Newtonsoft.Json;
using Y_Chat.Entities;

namespace Y_Chat.Middlewares;

public class CheckProfileMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            await _next(context);
            return;        
        }
        
        var user = (User)context.Items["User"];

        if (!user.ProfileDetailsAdded)
        {
            await ReturnUnauthorizedResponse(context, "Profile details not added");
        }
        
        await _next(context);
    }
    
    private async Task ReturnUnauthorizedResponse(HttpContext context, string message)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";
        var response = new { error = message };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}