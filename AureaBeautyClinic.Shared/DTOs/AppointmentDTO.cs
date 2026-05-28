using AureaBeautyClinic.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public record AppointmentDTO(
		int appointmentID,
		int userID,
		int doctorID,
		DateTime scheduled,
		string state,
		string? notes,
		DateTime created,
		UserDTO user,
		DoctorDTO doctor
	);
}
