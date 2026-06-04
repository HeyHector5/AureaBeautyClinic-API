using AureaBeautyClinic.Business.Mappings;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using AureaBeautyClinic.Shared.Interfaces.IServices;

namespace AureaBeautyClinic.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => u.ToDto());
        }

        public async Task<UserDTO?> GetByIdAsync(int UserId)
        {
            var user = await _userRepository.GetByIdAsync(UserId);
            return user?.ToDto();
        }

        public async Task<UserDTO> CreateAsync(User user)
        {
            var created = await _userRepository.CreateUserAsync(user);
            return created.ToDto();
        }

        public async Task<UserDTO?> UpdateAsync(int UserId, string? name, string? lastName, string? phone, bool? isActive)
        {
            var existing = await _userRepository.GetByIdAsync(UserId);
            if (existing is null) return null;

            if (name is not null) existing.FirstName = name;
            if (lastName is not null) existing.LastName = lastName;
            if (phone is not null) existing.Phone = phone;
            if (isActive is not null) existing.IsActive = isActive.Value;

            await _userRepository.UpdateAsync(existing);
            return existing.ToDto();
        }

        public async Task<bool> ExistsAsync(User user) =>
            await _userRepository.ExistsAsync(user);
    }
}
