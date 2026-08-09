using AureaBeautyClinic.Business.Mappings;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Exceptions;
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

        public async Task<UserDTO> CreateAsync(int RoleId, string name, string lastName, string email, string password, string? phone)
        {
            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing is not null)
                throw new EmailAlreadyRegisteredException(email);

            var user = new User
            {
                RoleId = RoleId,
                FirstName = name,
                LastName = lastName,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Phone = phone,
                Registered = DateTime.Now,
                IsActive = true
            };

            var created = await _userRepository.CreateUserAsync(user);

            // La entidad recién insertada no trae la navegación Role, que ToDto() desreferencia.
            var full = await _userRepository.GetByIdAsync(created.UserId)
                ?? throw new InvalidOperationException("User was created but could not be retrieved.");

            return full.ToDto();
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

        public async Task<UserDTO?> DeactivateAsync(int UserId) =>
            await UpdateAsync(UserId, null, null, null, false);

        public async Task<bool> ExistsAsync(User user) =>
            await _userRepository.ExistsAsync(user);
    }
}
