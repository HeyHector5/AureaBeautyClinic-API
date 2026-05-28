using System.ComponentModel.DataAnnotations;

namespace AureaBeautyClinic.API.Requests
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "RoleID is required.")]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "LastName must be between 2 and 100 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? Phone { get; set; }
    }

    public class UpdateUserRequest
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string? Name { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "LastName must be between 2 and 100 characters.")]
        public string? LastName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? Phone { get; set; }

        public bool? IsActive { get; set; }
    }
}
