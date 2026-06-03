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

        public async Task<IEnumerable<Doctor>> GetAllAsync() =>
            await _context.Doctors
                .Include(d => d.User)
                    .ThenInclude(u => u.Role)
                .Include(d => d.Specialty)
                .ToListAsync();

        public async Task<Doctor?> GetByIdAsync(int DoctorId) =>
            await _context.Doctors
                .Include(d => d.User)
                    .ThenInclude(u => u.Role)
                .Include(d => d.Specialty)
                .FirstOrDefaultAsync(d => d.DoctorId == DoctorId);

        public async Task<Doctor> CreateAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int DoctorId)
        {
            var doctor = await _context.Doctors.FindAsync(DoctorId);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
