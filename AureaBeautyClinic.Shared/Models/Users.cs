using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.Models
{
	public class Users
	{
		public int UserID { get; set; }
		public int RoleID { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public byte[] PasswordHash { get; set; }
		public string? Phone { get; set; }
		public DateTime RegisterDate { get; set; }
		public bool IsActive { get; set; }

		public Roles Role { get; set; }
	}
}
