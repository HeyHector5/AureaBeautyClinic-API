using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.Models
{
	public class Doctor
	{
		public int DoctorID { get; set; }
		public int UserID { get; set; }
		public int SpecialtyID { get; set; }
		public string? LicenseNumber { get; set; }
		public string? Biography { get; set; }
		public string? PhotoURL { get; set; }
		public bool Active { get; set; }

		public User User { get; set; }
		public Specialty Specialty { get; set; }
	}
}
