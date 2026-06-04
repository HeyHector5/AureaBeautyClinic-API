using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AureaBeautyClinic.Shared.Entities
{
	public class Specialty
	{
		[Key]
		public int SpecialtyId { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public bool IsActive { get; set; }
	}
}
