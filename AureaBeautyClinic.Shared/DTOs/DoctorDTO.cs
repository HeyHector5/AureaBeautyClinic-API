using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public record DoctorDTO(
		int DoctorId,
		int UserId,
		int SpecialtyId,
		string? licenseNumber,
		string? biography,
		string? photoURL,
		bool isActive,
		UserDTO user,
		SpecialtyDTO specialty
	);
}
