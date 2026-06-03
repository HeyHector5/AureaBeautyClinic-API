using AureaBeautyClinic.Shared.DTOs;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(int RoleId, string name, string lastName, string email, string password, string? phone);
        Task<AuthResponse> LoginAsync(string email, string password);
    }
}
