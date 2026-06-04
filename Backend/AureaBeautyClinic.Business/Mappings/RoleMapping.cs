using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Business.Mappings
{
    public static class RoleMapping
    {
        public static RoleDTO ToDto(this Role role) => new(
            role.RoleId,
            role.Name,
            role.Description
        );
    }
}
