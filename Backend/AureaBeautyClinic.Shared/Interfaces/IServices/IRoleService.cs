using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task<RoleDTO?> GetByIdAsync(int RoleId);
        Task<RoleDTO> CreateAsync(Role role);
        Task UpdateAsync(Role role);
    }
}
