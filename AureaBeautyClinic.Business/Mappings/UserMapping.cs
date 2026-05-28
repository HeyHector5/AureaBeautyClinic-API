using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Business.Mappings
{
    public static class UserMapping
    {
        public static UserDTO ToDto(this Users user) => new(
            user.UserID,
            user.RoleID,
            user.Name,
            user.LastName,
            user.Email,
            user.PasswordHash,
            user.Phone,
            user.RegisterDate,
            user.IsActive,
            user.Role.ToDto()
        );
    }
}
