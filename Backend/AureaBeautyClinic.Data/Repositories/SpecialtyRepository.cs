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

        public async Task<IEnumerable<Specialty>> GetAllAsync() =>
            await _context.Specialties.ToListAsync();

        public async Task<Specialty?> GetByIdAsync(int SpecialtyId) =>
            await _context.Specialties.FindAsync(SpecialtyId);

        public async Task<Specialty> CreateAsync(Specialty specialty)
        {
            await _context.Specialties.AddAsync(specialty);
            await _context.SaveChangesAsync();
            return specialty;
        }

        public async Task UpdateAsync(Specialty specialty)
        {
            _context.Specialties.Update(specialty);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int SpecialtyId)
        {
            var specialty = await _context.Specialties.FindAsync(SpecialtyId);
            if (specialty != null)
            {
                _context.Specialties.Remove(specialty);
                await _context.SaveChangesAsync();
            }
        }
    }
}
