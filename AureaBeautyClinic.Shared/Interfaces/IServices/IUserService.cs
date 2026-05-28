using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface IUserService
    {
        Task<UserDTO> CreateAsync(Users user);
        Task UpdateAsync(int userId);
        Task<bool> ExistsAsync(Users user);
    }
}
