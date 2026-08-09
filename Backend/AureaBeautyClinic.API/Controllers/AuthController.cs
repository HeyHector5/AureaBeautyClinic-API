using AureaBeautyClinic.API.Requests;
using AureaBeautyClinic.Shared.Common;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AureaBeautyClinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Register([FromBody] RegisterRequest request)
        {
            // El rol no se acepta desde el cliente: el registro público siempre crea
            // un paciente. Las cuentas de Admin/Doctor se crean con POST /api/users.
            var result = await _authService.RegisterAsync(
                request.Name,
                request.LastName,
                request.Email,
                request.Password,
                request.Phone
            );
            return Ok(ApiResponse<AuthResponse>.Ok(result, "User registered successfully."));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request.Email, request.Password);
            return Ok(ApiResponse<AuthResponse>.Ok(result, "Login successful."));
        }
    }
}
