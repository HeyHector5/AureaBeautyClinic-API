using AureaBeautyClinic.Data.Contexts;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AureaBeautyClinic.Data.Repositories
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly ApplicationContext _context;

        public SpecialtyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Specialties>> GetAllAsync() =>
            await _context.Specialties.ToListAsync();

        public async Task<Specialties?> GetByIdAsync(int specialtyId) =>
            await _context.Specialties.FindAsync(specialtyId);

        public async Task<Specialties> CreateAsync(Specialties specialty)
        {
            await _context.Specialties.AddAsync(specialty);
            await _context.SaveChangesAsync();
            return specialty;
        }

        public async Task UpdateAsync(Specialties specialty)
        {
            _context.Specialties.Update(specialty);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int specialtyId)
        {
            var specialty = await _context.Specialties.FindAsync(specialtyId);
            if (specialty != null)
            {
                _context.Specialties.Remove(specialty);
                await _context.SaveChangesAsync();
            }
        }
    }
}
