using AureaBeautyClinic.Business.Mappings;
using AureaBeautyClinic.Shared.Constants;
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

            // La entidad recién insertada no trae las navegaciones User/Doctor,
            // y ToDto() las desreferencia. Se relee con los Include del repositorio.
            var full = await _appointmentRepository.GetByIdAsync(created.AppointmentId)
                ?? throw new InvalidOperationException("Appointment was created but could not be retrieved.");

            return full.ToDto();
        }

        public async Task UpdateAsync(Appointment appointment) =>
            await _appointmentRepository.UpdateAsync(appointment);

        public async Task<AppointmentDTO?> CancelAsync(int AppointmentId)
        {
            var existing = await _appointmentRepository.GetByIdAsync(AppointmentId);
            if (existing is null) return null;

            existing.State = Appointmenttatus.Cancelled;
            await _appointmentRepository.UpdateAsync(existing);

            return existing.ToDto();
        }
    }
}
