using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public class SpecialtyDTO
	{
		public int SpecialtyID { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public bool Active { get; set; }
	}
}
