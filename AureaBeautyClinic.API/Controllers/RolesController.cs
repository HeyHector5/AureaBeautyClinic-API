using AureaBeautyClinic.API.Requests;
using AureaBeautyClinic.Shared.Common;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AureaBeautyClinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<RoleDTO>>>> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<RoleDTO>>.Ok(roles));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<RoleDTO>>> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role is null)
                return NotFound(ApiResponse<RoleDTO>.Fail($"Role with ID {id} was not found."));

            return Ok(ApiResponse<RoleDTO>.Ok(role));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<RoleDTO>>> Create([FromBody] CreateRoleRequest request)
        {
            var role = new Roles { Name = request.Name, Description = request.Description };
            var created = await _roleService.CreateAsync(role);

            return CreatedAtAction(nameof(GetById), new { id = created.roleID },
                ApiResponse<RoleDTO>.Ok(created, "Role created successfully."));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<RoleDTO>>> Update(int id, [FromBody] UpdateRoleRequest request)
        {
            var existing = await _roleService.GetByIdAsync(id);
            if (existing is null)
                return NotFound(ApiResponse<RoleDTO>.Fail($"Role with ID {id} was not found."));

            var role = new Roles { RoleID = id, Name = request.Name, Description = request.Description };
            await _roleService.UpdateAsync(role);

            var updated = existing with { name = request.Name, description = request.Description };
            return Ok(ApiResponse<RoleDTO>.Ok(updated, "Role updated successfully."));
        }
    }
}
