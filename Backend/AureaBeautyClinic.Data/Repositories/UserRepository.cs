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

		public async Task<IEnumerable<User>> GetAllAsync() =>
			await _context.Users
				.Include(u => u.Role)
				.ToListAsync();

		public async Task<User?> GetByIdAsync(int UserId) =>
			await _context.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.UserId == UserId);

		public async Task<User?> GetByEmailAsync(string email) =>
			await _context.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.Email == email);

		public async Task<User> CreateUserAsync(User user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			return user;
		}

		public async Task UpdateUserAsync(int UserId)
		{
			var user = await _context.Users.FindAsync(UserId);
			if (user != null)
			{
				_context.Users.Update(user);
				await _context.SaveChangesAsync();
			}
		}

		public async Task UpdateAsync(User user)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> ExistsAsync(User user) =>
			await _context.Users.AnyAsync(u => u.Email == user.Email);
	}
}
