using AureaBeautyClinic.Shared.DTOs;
using AureaBeautyClinic.Shared.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace AureaBeautyClinic.Business.Mappings
{
	public static class UserMapping
	{
		public static UserDTO MapToDto(Users user)
		{
			if (user == null) return null;

			return new UserDTO
			{
				UserID = user.UserID,
				RoleID = user.RoleID,
				Name = user.Name,
				LastName = user.LastName,
				FullName = $"{user.Name} {user.LastName}",
				Email = user.Email,
				PasswordHash = user.PasswordHash,
				PhoneNumber = user.Phone,
				RegisterDate = user.RegisterDate,
				IsActive = user.IsActive,
				//Role = user.Role
			};
		}
	}
}
