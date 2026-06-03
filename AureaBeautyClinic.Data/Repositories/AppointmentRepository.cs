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

        public async Task<IEnumerable<Appointment>> GetAllAsync() =>
            await _context.Appointments
                .Include(a => a.User)
                    .ThenInclude(u => u.Role)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Specialty)
                .ToListAsync();

        public async Task<Appointment?> GetByIdAsync(int AppointmentId) =>
            await _context.Appointments
                .Include(a => a.User)
                    .ThenInclude(u => u.Role)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Specialty)
                .FirstOrDefaultAsync(a => a.AppointmentId == AppointmentId);

        public async Task<IEnumerable<Appointment>> GetByUserIdAsync(int UserId) =>
            await _context.Appointments
                .Include(a => a.User)
                    .ThenInclude(u => u.Role)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Specialty)
                .Where(a => a.UserId == UserId)
                .ToListAsync();

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int DoctorId) =>
            await _context.Appointments
                .Include(a => a.User)
                    .ThenInclude(u => u.Role)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Specialty)
                .Where(a => a.DoctorId == DoctorId)
                .ToListAsync();

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int AppointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(AppointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
