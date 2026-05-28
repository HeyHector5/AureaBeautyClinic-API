using AureaBeautyClinic.Data.Contexts;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AureaBeautyClinic.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationContext _context;

        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Roles>> GetAllAsync() =>
            await _context.Roles.ToListAsync();

        public async Task<Roles?> GetByIdAsync(int roleId) =>
            await _context.Roles.FindAsync(roleId);

        public async Task<Roles> CreateAsync(Roles role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task UpdateAsync(Roles role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
