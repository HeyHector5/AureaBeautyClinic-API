using AureaBeautyClinic.Business.Mappings;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using AureaBeautyClinic.Shared.Interfaces.IServices;

namespace AureaBeautyClinic.Business.Services
{
    public class Appointmentervice : IAppointmentervice
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public Appointmentervice(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAllAsync()
        {
            var Appointment = await _appointmentRepository.GetAllAsync();
            return Appointment.Select(a => a.ToDto());
        }

        public async Task<AppointmentDTO?> GetByIdAsync(int AppointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(AppointmentId);
            return appointment?.ToDto();
        }

        public async Task<IEnumerable<AppointmentDTO>> GetByUserIdAsync(int UserId)
        {
            var Appointment = await _appointmentRepository.GetByUserIdAsync(UserId);
            return Appointment.Select(a => a.ToDto());
        }

        public async Task<IEnumerable<AppointmentDTO>> GetByDoctorIdAsync(int DoctorId)
        {
            var Appointment = await _appointmentRepository.GetByDoctorIdAsync(DoctorId);
            return Appointment.Select(a => a.ToDto());
        }

        public async Task<AppointmentDTO> CreateAsync(Appointment appointment)
        {
            var created = await _appointmentRepository.CreateAsync(appointment);
            return created.ToDto();
        }

        public async Task UpdateAsync(Appointment appointment) =>
            await _appointmentRepository.UpdateAsync(appointment);
    }
}
