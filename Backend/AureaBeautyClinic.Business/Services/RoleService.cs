using AureaBeautyClinic.Business.Mappings;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using AureaBeautyClinic.Shared.Interfaces.IServices;

namespace AureaBeautyClinic.Business.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return roles.Select(r => r.ToDto());
        }

        public async Task<RoleDTO?> GetByIdAsync(int RoleId)
        {
            var role = await _roleRepository.GetByIdAsync(RoleId);
            return role?.ToDto();
        }

        public async Task<RoleDTO> CreateAsync(Role role)
        {
            var created = await _roleRepository.CreateAsync(role);
            return created.ToDto();
        }

        public async Task UpdateAsync(Role role) =>
            await _roleRepository.UpdateAsync(role);
    }
}
