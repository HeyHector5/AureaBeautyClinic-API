using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(int userId);
        Task<UserDTO> CreateAsync(Users user);
        Task UpdateAsync(Users user);
        Task<bool> ExistsAsync(Users user);
    }
}
