using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IRepositories
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialties>> GetAllAsync();
        Task<Specialties?> GetByIdAsync(int specialtyId);
        Task<Specialties> CreateAsync(Specialties specialty);
        Task UpdateAsync(Specialties specialty);
    }
}
