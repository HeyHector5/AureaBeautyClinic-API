using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public record RoleDTO(
		int roleID,
		string name,
		string? description
	);
}
