using AureaBeautyClinic.Data.Contexts;
using AureaBeautyClinic.Shared.Interfaces.IRepositories;
using AureaBeautyClinic.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace AureaBeautyClinic.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationContext _context;

		public UserRepository(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Users>> GetAllAsync() =>
			await _context.Users
				.Include(u => u.Role)
				.ToListAsync();

		public async Task<Users?> GetByIdAsync(int userId) =>
			await _context.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.UserID == userId);

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

		public async Task UpdateAsync(Users user)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> ExistsAsync(Users user) =>
			await _context.Users.AnyAsync(u => u.Email == user.Email);
	}
}
