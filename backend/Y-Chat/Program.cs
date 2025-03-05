using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Y_Chat.Entities;
using Y_Chat.Helpers;
using Y_Chat.Middlewares;
using Y_Chat.Services;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

// Add services to the container.

builder.Services.AddLogging();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddScoped<JwtTokenHelper>();

builder.Services.AddScoped<IEmailTokenService, EmailTokenService>();
builder.Services.AddScoped<IEmailSendingService, EmailSendingService>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IImagesUploadService, ImagesUploadService>();
builder.Services.AddScoped<IValidateGoogleCaptchaService, ValidateGoogleCaptchaService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin"); 

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CheckEmailMiddleware>();

app.UseMiddleware<CheckProfileMiddleware>();

app.UseStaticFiles();

app.MapControllers();

app.Run();