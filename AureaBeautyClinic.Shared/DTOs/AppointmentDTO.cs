using AureaBeautyClinic.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public record AppointmentDTO(
		int AppointmentId,
		int UserId,
		int DoctorId,
		DateTime scheduled,
		string state,
		string? notes,
		DateTime created,
		UserDTO user,
		DoctorDTO doctor
	);
}
