using AureaBeautyClinic.Data.Contexts;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AureaBeautyClinic.Data.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationContext _context;

        public AppointmentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointments>> GetAllAsync() =>
            await _context.Appointments
                .Include(a => a.User)
                    .ThenInclude(u => u.Role)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Specialty)
                .ToListAsync();

        public async Task<Appointments?> GetByIdAsync(int appointmentId) =>
            await _context.Appointments
                .Include(a => a.User)
                    .ThenInclude(u => u.Role)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Specialty)
                .FirstOrDefaultAsync(a => a.AppointmentID == appointmentId);

        public async Task<IEnumerable<Appointments>> GetByUserIdAsync(int userId) =>
            await _context.Appointments
                .Include(a => a.User)
                    .ThenInclude(u => u.Role)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Specialty)
                .Where(a => a.UserID == userId)
                .ToListAsync();

        public async Task<IEnumerable<Appointments>> GetByDoctorIdAsync(int doctorId) =>
            await _context.Appointments
                .Include(a => a.User)
                    .ThenInclude(u => u.Role)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Specialty)
                .Where(a => a.DoctorID == doctorId)
                .ToListAsync();

        public async Task<Appointments> CreateAsync(Appointments appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task UpdateAsync(Appointments appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
