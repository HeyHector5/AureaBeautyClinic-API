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

        public async Task<UserDTO> CreateAsync(Users user)
        {
            var created = await _userRepository.CreateUserAsync(user);
            return created.ToDto();
        }

        public async Task UpdateAsync(int userId) =>
            await _userRepository.UpdateUserAsync(userId);

        public async Task<bool> ExistsAsync(Users user) =>
            await _userRepository.ExistsAsync(user);
    }
}
