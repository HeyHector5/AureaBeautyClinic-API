using AureaBeautyClinic.Data.Contexts;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using AureaBeautyClinic.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AureaBeautyClinic.Data.Repositories
{
	public class UserRepository : ApplicationContext, IUserRepository
	{
		private ApplicationContext _context;

		public UserRepository(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<Users> CreateUserAsync(Users user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			return user;
		}

		public async Task UpdateUserAsync(int userId)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user != null)
			{
				_context.Users.Update(user);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<bool> ExistsAsync(Users user)
		{
			var exists = await _context.Users.AnyAsync(u => u.Email == user.Email);
			if (exists)
			{
				return true;
			}
			return false;
		}
	}
}
