using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Business.Mappings
{
    public static class DoctorMapping
    {
        public static DoctorDTO ToDto(this Doctors doctor) => new(
            doctor.DoctorID,
            doctor.UserID,
            doctor.SpecialtyID,
            doctor.LicenseNumber,
            doctor.Biography,
            doctor.PhotoURL,
            doctor.IsActive,
            doctor.User.ToDto(),
            doctor.Specialty.ToDto()
        );
    }
}
