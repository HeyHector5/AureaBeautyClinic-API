using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AureaBeautyClinic.Shared.Entities
{
	public class Doctor
	{
		[Key]
		public int DoctorId { get; set; }
		public int UserId { get; set; }
		public int SpecialtyId { get; set; }
		public string? LicenseNumber { get; set; }
		public string? Biography { get; set; }
		public string? PhotoURL { get; set; }
		public bool IsActive { get; set; }

		public User User { get; set; }
		public Specialty Specialty { get; set; }
	}
}
