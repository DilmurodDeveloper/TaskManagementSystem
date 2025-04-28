using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Register model validation failed: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(registerDto);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Register attempt failed: {@RegisterDto}, Reason: {Reason}", registerDto, result.Message);
                return BadRequest(result.Message);
            }

            _logger.LogInformation("User registered successfully: {@RegisterDto}", registerDto);
            return Ok(result.Message);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Login model validation failed: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var token = await _authService.LoginAsync(loginDto);

            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Login attempt failed for user: {Username}", loginDto.Username);
                return Unauthorized("Login yoki parol xato!");
            }

            _logger.LogInformation("User logged in successfully: {Username}", loginDto.Username);
            return Ok(new { Token = token });
        }

    }
}
