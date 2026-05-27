using AureaBeautyClinic.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public class AppointmentDTO
	{
		public int AppointmentID { get; set; }
		public int UserID { get; set; }
		public int DoctorID { get; set; }
		public DateTime DateHour { get; set; }
		public string State { get; set; }
		public string? Notes { get; set; }
		public DateTime CreationDate { get; set; }

		public UserDTO User { get; set; }
		public DoctorDTO Doctor { get; set; }
	}
}
