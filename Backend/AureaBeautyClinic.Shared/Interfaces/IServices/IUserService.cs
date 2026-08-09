using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(int UserId);
        Task<UserDTO> CreateAsync(int RoleId, string name, string lastName, string email, string password, string? phone);
        Task<UserDTO?> UpdateAsync(int UserId, string? name, string? lastName, string? phone, bool? isActive);
        Task<UserDTO?> DeactivateAsync(int UserId);
        Task<bool> ExistsAsync(User user);
    }
}
