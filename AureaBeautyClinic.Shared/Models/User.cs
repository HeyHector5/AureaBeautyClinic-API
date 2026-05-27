using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.Models
{
	public class User
	{
		public int UserID { get; set; }
		public int RoleID { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public byte[] PasswordHash { get; set; }
		public string? PhoneNumber { get; set; }
		public DateTime RegisterDate { get; set; }
		public bool Active { get; set; }

		public Role Role { get; set; }
	}
}
