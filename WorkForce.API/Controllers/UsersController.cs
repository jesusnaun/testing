using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkForce.API.Models.Responses;
using WorkForce.API.Models.User;
using WorkForce.API.Repositories;

namespace WorkForce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateUser(UserCreateDto userDto)
        {
            if (!ModelState.IsValid)
            {
                //var errors = ModelState.Values
                //    .SelectMany(v => v.Errors)
                //    .Select(e => e.ErrorMessage)
                //    .ToList();

                // Combinar todos los errores en un solo mensaje
                var errorMessages = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return BadRequest(ApiResponse<int>.ErrorResult($"Datos inválidos: {errorMessages}"));
            }

            var result = await _userRepository.CreateUserAsync(userDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return CreatedAtAction(nameof(GetUser), new { id = result.Data }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<User>>> GetUser(int id)
        {
            // Implementar obtención de usuario
            return Ok(ApiResponse<User>.SuccessResult(new User()));
        }
    }
}
