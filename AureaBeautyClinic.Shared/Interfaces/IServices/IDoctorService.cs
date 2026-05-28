using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDTO>> GetAllAsync();
        Task<DoctorDTO?> GetByIdAsync(int doctorId);
        Task<DoctorDTO> CreateAsync(Doctors doctor);
        Task UpdateAsync(Doctors doctor);
    }
}
