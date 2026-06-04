using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Business.Mappings
{
    public static class SpecialtyMapping
    {
        public static SpecialtyDTO ToDto(this Specialty specialty) => new(
            specialty.SpecialtyId,
            specialty.Name,
            specialty.Description,
            specialty.IsActive
        );
    }
}
