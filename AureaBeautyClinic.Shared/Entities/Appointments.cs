using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.Entities
{
	public class Appointments
	{
		public int AppointmentID { get; set; }
		public int UserID { get; set; }
		public int DoctorID { get; set; }
		public DateTime Scheduled { get; set; }
		public string State { get; set; } = string.Empty;
		public string? Notes { get; set; }
		public DateTime Created{ get; set; }

		public Users User { get; set; }
		public Doctors Doctor { get; set; }
	}
}
