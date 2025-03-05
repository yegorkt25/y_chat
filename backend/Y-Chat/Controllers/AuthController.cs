using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Y_Chat.DTOs;
using Y_Chat.Services;

namespace Y_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthenticationService authenticationService, ILogger<AuthController> logger) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginDto)
        {
            try
            {
                logger.LogInformation("email: {}; password: {}", loginDto.Email, loginDto.Password);
                var bearer = await authenticationService.Login(loginDto);
                
                return Ok(bearer);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO registerDto)
        {
            try
            {
                // logger.LogInformation("email: {}; password: {}; captcha: {}", registerDto.Email, registerDto.Password, registerDto.Captcha);
                var token = await authenticationService.Register(registerDto);
                
                return Ok(token);
            } 
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpGet("confirm-email")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string token)
        {
            try
            {
                var emailConfirmed = await authenticationService.ConfirmEmail(token);
                
                return Redirect("http://localhost:5173/");
            } 
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [Authorize]
        [HttpPost("add-profile-details")]
        public async Task<ActionResult> AddProfileDetails(AddProfileDetailsDTO dto)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                if (token.StartsWith("Bearer "))
                {
                    // Remove the "Bearer " part of the token
                    token = token.Substring(7).Trim();
                }
                
                var profile = await authenticationService.AddProfileDetails(dto, token);
                
                return Ok(profile);
            } 
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}
