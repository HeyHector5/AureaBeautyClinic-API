using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IServices
{
    public interface ISpecialtyService
    {
        Task<IEnumerable<SpecialtyDTO>> GetAllAsync();
        Task<SpecialtyDTO?> GetByIdAsync(int SpecialtyId);
        Task<SpecialtyDTO> CreateAsync(Specialty specialty);
        Task UpdateAsync(Specialty specialty);
    }
}
