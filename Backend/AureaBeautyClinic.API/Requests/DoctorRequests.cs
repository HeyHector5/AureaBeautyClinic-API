using System.ComponentModel.DataAnnotations;

namespace AureaBeautyClinic.API.Requests
{
    public class CreateDoctorRequest
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "SpecialtyId is required.")]
        public int SpecialtyId { get; set; }

        [StringLength(50, ErrorMessage = "LicenseNumber cannot exceed 50 characters.")]
        public string? LicenseNumber { get; set; }

        [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters.")]
        public string? Biography { get; set; }

        [Url(ErrorMessage = "PhotoURL must be a valid URL.")]
        public string? PhotoURL { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateDoctorRequest
    {
        [Required(ErrorMessage = "SpecialtyId is required.")]
        public int SpecialtyId { get; set; }

        [StringLength(50, ErrorMessage = "LicenseNumber cannot exceed 50 characters.")]
        public string? LicenseNumber { get; set; }

        [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters.")]
        public string? Biography { get; set; }

        [Url(ErrorMessage = "PhotoURL must be a valid URL.")]
        public string? PhotoURL { get; set; }

        public bool IsActive { get; set; }
    }
}
