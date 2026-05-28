using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.Entities
{
	public class Doctors
	{
		public int DoctorID { get; set; }
		public int UserID { get; set; }
		public int SpecialtyID { get; set; }
		public string? LicenseNumber { get; set; }
		public string? Biography { get; set; }
		public string? PhotoURL { get; set; }
		public bool IsActive { get; set; }

		public Users User { get; set; }
		public Specialties Specialty { get; set; }
	}
}
