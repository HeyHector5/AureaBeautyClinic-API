using AureaBeautyClinic.Business.Mappings;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using AureaBeautyClinic.Shared.Interfaces.IServices;

namespace AureaBeautyClinic.Business.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAllAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            return appointments.Select(a => a.ToDto());
        }

        public async Task<AppointmentDTO?> GetByIdAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            return appointment?.ToDto();
        }

        public async Task<IEnumerable<AppointmentDTO>> GetByUserIdAsync(int userId)
        {
            var appointments = await _appointmentRepository.GetByUserIdAsync(userId);
            return appointments.Select(a => a.ToDto());
        }

        public async Task<IEnumerable<AppointmentDTO>> GetByDoctorIdAsync(int doctorId)
        {
            var appointments = await _appointmentRepository.GetByDoctorIdAsync(doctorId);
            return appointments.Select(a => a.ToDto());
        }

        public async Task<AppointmentDTO> CreateAsync(Appointments appointment)
        {
            var created = await _appointmentRepository.CreateAsync(appointment);
            return created.ToDto();
        }

        public async Task UpdateAsync(Appointments appointment) =>
            await _appointmentRepository.UpdateAsync(appointment);

        public async Task DeleteAsync(int appointmentId) =>
            await _appointmentRepository.DeleteAsync(appointmentId);
    }
}
