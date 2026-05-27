using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.Models
{
	public class Appointment
	{
		public int AppointmentID { get; set; }
		public int UserID { get; set; }
		public int DoctorID { get; set; }
		public DateTime DateHour { get; set; }
		public string State { get; set; }
		public string? Notes { get; set; }
		public DateTime CreationDate { get; set; }

		public User User { get; set; }
		public Doctor Doctor { get; set; }
	}
}
