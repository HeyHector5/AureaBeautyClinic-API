using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public record UserDTO(
		int userID,
		int roleID,
		string name,
		string lastName,
		string email,
		byte[] passwordHash,
		string? phoneNumber,
		DateTime registerDate,
		bool isActive,
		RoleDTO role
	)
	{
		public string FullName => $"{name} {lastName}";
	};
}
