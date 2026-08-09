using System.Security.Claims;
using AureaBeautyClinic.API.Requests;
using AureaBeautyClinic.Shared.Common;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AureaBeautyClinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        private bool IsAdmin => User.IsInRole("Admin");

        private int? CurrentUserId =>
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

        /// <summary>Un usuario sólo puede tocar su propio registro; el Admin puede tocar cualquiera.</summary>
        private bool CanAccess(int userId) => IsAdmin || CurrentUserId == userId;

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<UserDTO>>.Ok(users));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> GetById(int id)
        {
            if (!CanAccess(id))
                return Forbid();

            var user = await _userService.GetByIdAsync(id);
            if (user is null)
                return NotFound(ApiResponse<UserDTO>.Fail($"User with ID {id} was not found."));

            return Ok(ApiResponse<UserDTO>.Ok(user));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Create([FromBody] CreateUserRequest request)
        {
            var created = await _userService.CreateAsync(
                request.RoleId,
                request.Name,
                request.LastName,
                request.Email,
                request.Password,
                request.Phone
            );

            return CreatedAtAction(nameof(GetById), new { id = created.UserId },
                ApiResponse<UserDTO>.Ok(created, "User created successfully."));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Update(int id, [FromBody] UpdateUserRequest request)
        {
            if (!CanAccess(id))
                return Forbid();

            // Sólo un Admin puede activar/desactivar cuentas.
            var isActive = IsAdmin ? request.IsActive : null;

            var updated = await _userService.UpdateAsync(id, request.Name, request.LastName, request.Phone, isActive);
            if (updated is null)
                return NotFound(ApiResponse<UserDTO>.Fail($"User with ID {id} was not found."));

            return Ok(ApiResponse<UserDTO>.Ok(updated, "User updated successfully."));
        }

        /// <summary>
        /// Soft delete: marca el usuario como inactivo. La fila nunca se elimina.
        /// </summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Deactivate(int id)
        {
            var deactivated = await _userService.DeactivateAsync(id);
            if (deactivated is null)
                return NotFound(ApiResponse<UserDTO>.Fail($"User with ID {id} was not found."));

            return Ok(ApiResponse<UserDTO>.Ok(deactivated, "User deactivated successfully."));
        }
    }
}
