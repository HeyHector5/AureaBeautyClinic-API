using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AureaBeautyClinic.Shared.Entities
{
	public class User
	{
		[Key]
		public int UserId { get; set; }
		public int RoleId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; } = string.Empty;
		public string? Phone { get; set; }
		public DateTime Registered { get; set; }
		public bool IsActive { get; set; }

		public Role Role { get; set; }
	}
}
