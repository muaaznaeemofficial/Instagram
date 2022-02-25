using Backend.DTO.User;
using Backend.Services.Interfaces;
using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationManager _authManager;
        private readonly ILoggerManager _logger;
        public AccountController(IAuthenticationManager authManager, ILoggerManager logger)
        {
            _authManager = authManager;
            _logger = logger;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {

            try
            {
                if (!await _authManager.ValidateUser(user))
                {
                    _logger.LogWarn($" {nameof(Authenticate)}Authentication failed, Wrong Username or password.");
                    return Unauthorized();
                }
                return Ok(new { Token = await _authManager.CreateToken() });
            }
            catch (Exception e)
            {
                _logger.LogError($" {nameof(Authenticate)} {e.Message}");
                return BadRequest(new { message = "Something bad happened." });
            }

        }




    }
}
