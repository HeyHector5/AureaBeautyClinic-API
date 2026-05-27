using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.DTOs
{
	public class UserDTO
	{
		public int UserID { get; set; }
		public int RoleID { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string? FullName { get; set; }
		public string Email { get; set; }
		public byte[] PasswordHash { get; set; }
		public string? PhoneNumber { get; set; }
		public DateTime RegisterDate { get; set; }
		public bool IsActive { get; set; }

		public RoleDTO Role { get; set; }

		public UserDTO()
		{
			FullName = $"{Name} {LastName}";
		}
	}
}
