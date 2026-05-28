using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface ISpecialtyService
    {
        Task<IEnumerable<SpecialtyDTO>> GetAllAsync();
        Task<SpecialtyDTO?> GetByIdAsync(int specialtyId);
        Task<SpecialtyDTO> CreateAsync(Specialties specialty);
        Task UpdateAsync(Specialties specialty);
    }
}
