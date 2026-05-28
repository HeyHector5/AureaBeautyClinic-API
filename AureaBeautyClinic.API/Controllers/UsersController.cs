using AureaBeautyClinic.API.Requests;
using AureaBeautyClinic.Shared.Common;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace AureaBeautyClinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<UserDTO>>.Ok(users));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is null)
                return NotFound(ApiResponse<UserDTO>.Fail($"User with ID {id} was not found."));

            return Ok(ApiResponse<UserDTO>.Ok(user));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Create([FromBody] CreateUserRequest request)
        {
            if (await _userService.ExistsAsync(new Users { Email = request.Email }))
                return Conflict(ApiResponse<UserDTO>.Fail("A user with this email already exists."));

            var user = new Users
            {
                RoleID = request.RoleID,
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = SHA256.HashData(Encoding.UTF8.GetBytes(request.Password)),
                Phone = request.Phone,
                RegisterDate = DateTime.UtcNow,
                IsActive = true
            };
            var created = await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(GetById), new { id = created.userID },
                ApiResponse<UserDTO>.Ok(created, "User created successfully."));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Update(int id, [FromBody] UpdateUserRequest request)
        {
            var existing = await _userService.GetByIdAsync(id);
            if (existing is null)
                return NotFound(ApiResponse<UserDTO>.Fail($"User with ID {id} was not found."));

            var user = new Users
            {
                UserID = id,
                RoleID = existing.roleID,
                Name = request.Name ?? existing.name,
                LastName = request.LastName ?? existing.lastName,
                Email = existing.email,
                PasswordHash = existing.passwordHash,
                Phone = request.Phone ?? existing.phoneNumber,
                RegisterDate = existing.registerDate,
                IsActive = request.IsActive ?? existing.isActive
            };
            await _userService.UpdateAsync(user);

            var updated = existing with
            {
                name = request.Name ?? existing.name,
                lastName = request.LastName ?? existing.lastName,
                phoneNumber = request.Phone ?? existing.phoneNumber,
                isActive = request.IsActive ?? existing.isActive
            };
            return Ok(ApiResponse<UserDTO>.Ok(updated, "User updated successfully."));
        }
    }
}
