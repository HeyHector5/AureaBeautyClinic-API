using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IRepositories
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetAllAsync();
        Task<Specialty?> GetByIdAsync(int SpecialtyId);
        Task<Specialty> CreateAsync(Specialty specialty);
        Task UpdateAsync(Specialty specialty);
    }
}
