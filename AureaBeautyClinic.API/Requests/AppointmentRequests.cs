using System.ComponentModel.DataAnnotations;

namespace AureaBeautyClinic.API.Requests
{
    public class CreateAppointmentRequest
    {
        [Required(ErrorMessage = "UserID is required.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "DoctorID is required.")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Scheduled date is required.")]
        public DateTime Scheduled { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
    }

    public class UpdateAppointmentRequest
    {
        [Required(ErrorMessage = "Scheduled date is required.")]
        public DateTime Scheduled { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters.")]
        public string State { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
    }
}
