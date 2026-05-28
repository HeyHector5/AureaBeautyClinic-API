using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public record SpecialtyDTO(
		int specialtyID,
		string name,
		string? description,
		bool isActive
	);
}
