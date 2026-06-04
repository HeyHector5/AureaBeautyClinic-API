using AureaBeautyClinic.API.Requests;
using AureaBeautyClinic.Shared.Common;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<UserDTO>>> Update(int id, [FromBody] UpdateUserRequest request)
        {
            var updated = await _userService.UpdateAsync(id, request.Name, request.LastName, request.Phone, request.IsActive);
            if (updated is null)
                return NotFound(ApiResponse<UserDTO>.Fail($"User with ID {id} was not found."));

            return Ok(ApiResponse<UserDTO>.Ok(updated, "User updated successfully."));
        }
    }
}
