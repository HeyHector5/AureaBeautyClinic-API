using AureaBeautyClinic.Business.Mappings;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using AureaBeautyClinic.Shared.Interfaces.IServices;

namespace AureaBeautyClinic.Business.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<DoctorDTO>> GetAllAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync();
            return doctors.Select(d => d.ToDto());
        }

        public async Task<DoctorDTO?> GetByIdAsync(int DoctorId)
        {
            var doctor = await _doctorRepository.GetByIdAsync(DoctorId);
            return doctor?.ToDto();
        }

        public async Task<DoctorDTO> CreateAsync(Doctor doctor)
        {
            var created = await _doctorRepository.CreateAsync(doctor);

            // La entidad recién insertada no trae las navegaciones User/Specialty,
            // y ToDto() las desreferencia. Se relee con los Include del repositorio.
            var full = await _doctorRepository.GetByIdAsync(created.DoctorId)
                ?? throw new InvalidOperationException("Doctor was created but could not be retrieved.");

            return full.ToDto();
        }

        public async Task UpdateAsync(Doctor doctor) =>
            await _doctorRepository.UpdateAsync(doctor);

        public async Task<DoctorDTO?> DeactivateAsync(int DoctorId)
        {
            var existing = await _doctorRepository.GetByIdAsync(DoctorId);
            if (existing is null) return null;

            existing.IsActive = false;
            await _doctorRepository.UpdateAsync(existing);

            return existing.ToDto();
        }
    }
}
