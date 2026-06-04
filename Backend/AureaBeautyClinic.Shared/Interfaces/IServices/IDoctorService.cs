using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDTO>> GetAllAsync();
        Task<DoctorDTO?> GetByIdAsync(int DoctorId);
        Task<DoctorDTO> CreateAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
    }
}
