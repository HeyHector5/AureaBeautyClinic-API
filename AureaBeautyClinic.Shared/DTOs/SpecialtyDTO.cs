using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public record SpecialtyDTO(
		int SpecialtyId,
		string name,
		string? description,
		bool isActive
	);
}
