using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Business.Mappings
{
    public static class RoleMapping
    {
        public static RoleDTO ToDto(this Roles role) => new(
            role.RoleID,
            role.Name,
            role.Description
        );
    }
}
