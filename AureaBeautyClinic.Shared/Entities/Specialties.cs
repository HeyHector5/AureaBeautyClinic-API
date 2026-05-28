using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.Entities
{
	public class Specialties
	{
		public int SpecialtyID { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public bool IsActive { get; set; }
	}
}
