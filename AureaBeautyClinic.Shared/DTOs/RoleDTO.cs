using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public record RoleDTO(
		int RoleId,
		string name,
		string? description
	);
}
