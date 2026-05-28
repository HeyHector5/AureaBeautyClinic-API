using AureaBeautyClinic.Business.Mappings;
using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using AureaBeautyClinic.Shared.Interfaces.IServices;

namespace AureaBeautyClinic.Business.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyService(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public async Task<IEnumerable<SpecialtyDTO>> GetAllAsync()
        {
            var specialties = await _specialtyRepository.GetAllAsync();
            return specialties.Select(s => s.ToDto());
        }

        public async Task<SpecialtyDTO?> GetByIdAsync(int specialtyId)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(specialtyId);
            return specialty?.ToDto();
        }

        public async Task<SpecialtyDTO> CreateAsync(Specialties specialty)
        {
            var created = await _specialtyRepository.CreateAsync(specialty);
            return created.ToDto();
        }

        public async Task UpdateAsync(Specialties specialty) =>
            await _specialtyRepository.UpdateAsync(specialty);
    }
}
