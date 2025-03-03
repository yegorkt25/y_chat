using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Y_Chat.DTOs;
using Y_Chat.Services;

namespace Y_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginDto)
        {
            try
            {
                var bearer = await authenticationService.Login(loginDto);
                
                return Ok(bearer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO loginDto)
        {
            try
            {
                var token = await authenticationService.Register(loginDto);
                
                return Ok(token);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("confirm-email")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string token)
        {
            try
            {
                var emailConfirmed = await authenticationService.ConfirmEmail(token);
                
                return Ok(emailConfirmed);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-profile-details")]
        public async Task<ActionResult> AddProfileDetails(AddProfileDetailsDTO dto)
        {
            try
            {
                var profile = await authenticationService.AddProfileDetails(dto);
                
                return Ok(profile);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
