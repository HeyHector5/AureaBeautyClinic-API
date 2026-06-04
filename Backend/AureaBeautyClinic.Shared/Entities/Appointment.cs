using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AureaBeautyClinic.Shared.Entities
{
	public class Appointment
	{
		[Key]
		public int AppointmentId { get; set; }
		public int UserId { get; set; }
		public int DoctorId { get; set; }
		public DateTime Scheduled { get; set; }
		public string State { get; set; } = string.Empty;
		public string? Notes { get; set; }
		public DateTime Created{ get; set; }

		public User User { get; set; }
		public Doctor Doctor { get; set; }
	}
}
