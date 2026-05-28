using AureaBeautyClinic.Data.Contexts;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AureaBeautyClinic.Data.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationContext _context;

        public DoctorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctors>> GetAllAsync() =>
            await _context.Doctors
                .Include(d => d.User)
                    .ThenInclude(u => u.Role)
                .Include(d => d.Specialty)
                .ToListAsync();

        public async Task<Doctors?> GetByIdAsync(int doctorId) =>
            await _context.Doctors
                .Include(d => d.User)
                    .ThenInclude(u => u.Role)
                .Include(d => d.Specialty)
                .FirstOrDefaultAsync(d => d.DoctorID == doctorId);

        public async Task<Doctors> CreateAsync(Doctors doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task UpdateAsync(Doctors doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
