using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IRepositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Roles>> GetAllAsync();
        Task<Roles?> GetByIdAsync(int roleId);
        Task<Roles> CreateAsync(Roles role);
        Task UpdateAsync(Roles role);
    }
}
