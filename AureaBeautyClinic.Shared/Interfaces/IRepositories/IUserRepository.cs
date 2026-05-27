using AureaBeautyClinic.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Shared.Interfaces.IRepositories
{
	public interface IUserRepository
	{
		Task<Users> CreateUserAsync(Users user);
		Task UpdateUserAsync(int userId);
		Task<bool> ExistsAsync(Users user);
	}
}
