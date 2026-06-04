using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Business.Mappings
{
    public static class UserMapping
    {
        public static UserDTO ToDto(this User user) => new(
            user.UserId,
            user.RoleId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Phone,
            user.Registered,
            user.IsActive,
            user.Role.ToDto()
        );
    }
}
