using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AureaBeautyClinic.Shared.Entities
{
	public class Role
	{
		[Key]
		public int RoleId { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
	}
}
