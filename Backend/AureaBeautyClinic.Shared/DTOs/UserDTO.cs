using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public record UserDTO(
		int UserId,
		int RoleId,
		string name,
		string lastName,
		string email,
		string? phoneNumber,
		DateTime registerDate,
		bool isActive,
		RoleDTO role
	)
	{
		public string FullName => $"{name} {lastName}";
	};
}
