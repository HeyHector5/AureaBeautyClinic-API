using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IRepositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctors>> GetAllAsync();
        Task<Doctors?> GetByIdAsync(int doctorId);
        Task<Doctors> CreateAsync(Doctors doctor);
        Task UpdateAsync(Doctors doctor);
    }
}
