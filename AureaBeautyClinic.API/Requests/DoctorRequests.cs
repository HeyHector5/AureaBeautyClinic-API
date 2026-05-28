using System.ComponentModel.DataAnnotations;

namespace AureaBeautyClinic.API.Requests
{
    public class CreateDoctorRequest
    {
        [Required(ErrorMessage = "UserID is required.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "SpecialtyID is required.")]
        public int SpecialtyID { get; set; }

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
        [Required(ErrorMessage = "SpecialtyID is required.")]
        public int SpecialtyID { get; set; }

        [StringLength(50, ErrorMessage = "LicenseNumber cannot exceed 50 characters.")]
        public string? LicenseNumber { get; set; }

        [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters.")]
        public string? Biography { get; set; }

        [Url(ErrorMessage = "PhotoURL must be a valid URL.")]
        public string? PhotoURL { get; set; }

        public bool IsActive { get; set; }
    }
}
