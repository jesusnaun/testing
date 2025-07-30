using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using WorkForce.API.Models.Auth;
using WorkForce.API.Models.Responses;
using WorkForce.API.Repositories;
using WorkForce.API.Services;

namespace WorkForce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
    private readonly IAuthRepository _authRepository;
    private readonly IJwtService _jwtService;

    public AuthController(IAuthRepository authRepository, IJwtService jwtService)
    {
        _authRepository = authRepository;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] LoginRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(ApiResponse<LoginResponse>.ErrorResult("Username and password are required"));
            }

                // Obtener el usuario por username (sin verificar password aún)
                var user = await _authRepository.ValidateUserAsync(request.Username);

                if (user == null || !user.Active)
                {
                    return Unauthorized(ApiResponse<LoginResponse>.ErrorResult("Invalid credentials"));
                }

                // Verificar la contraseña usando BCrypt
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

                if (!isPasswordValid)
                {
                    //return Unauthorized(ApiResponse<LoginResponse>.ErrorResult("Invalid credentials"));
                    return Ok(ApiResponse<LoginResponse>.ErrorResult("Invalid credentials"));
                }



                // Generate JWT token
                var token = _jwtService.GenerateToken(user);
                var expiration = DateTime.UtcNow.AddHours(8); // Should match JWT configuration

                var loginResponse = new LoginResponse
                {
                    Token = token,
                    Expiration = expiration,
                    User = user
                };

                return Ok(ApiResponse<LoginResponse>.SuccessResult(loginResponse, "Login successful"));
            }
            catch (Exception ex)
            {
                //return StatusCode(500, ApiResponse<LoginResponse>.ErrorResult($"Internal server error: {ex.Message}"));
                return Ok(ApiResponse<LoginResponse>.ErrorResult($"Internal server error: {ex.Message}"));
            }
        }


    }
}
