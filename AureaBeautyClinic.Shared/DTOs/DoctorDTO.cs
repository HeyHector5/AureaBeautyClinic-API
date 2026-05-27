using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public class DoctorDTO
	{
		public int DoctorID { get; set; }
		public int UserID { get; set; }
		public int SpecialtyID { get; set; }
		public string? LicenseNumber { get; set; }
		public string? Biography { get; set; }
		public string? PhotoURL { get; set; }
		public bool Active { get; set; }

		public UserDTO User { get; set; }
		public SpecialtyDTO Specialty { get; set; }
	}
}
