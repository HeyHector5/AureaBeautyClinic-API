using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Business.Mappings
{
    public static class DoctorMapping
    {
        public static DoctorDTO ToDto(this Doctor doctor) => new(
            doctor.DoctorId,
            doctor.UserId,
            doctor.SpecialtyId,
            doctor.LicenseNumber,
            doctor.Biography,
            doctor.PhotoURL,
            doctor.IsActive,
            doctor.User.ToDto(),
            doctor.Specialty.ToDto()
        );
    }
}
