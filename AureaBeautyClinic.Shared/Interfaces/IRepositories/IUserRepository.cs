using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IRepositories
{
	public interface IUserRepository
	{
		Task<IEnumerable<User>> GetAllAsync();
		Task<User?> GetByIdAsync(int UserId);
		Task<User?> GetByEmailAsync(string email);
		Task<User> CreateUserAsync(User user);
		Task UpdateUserAsync(int UserId);
		Task UpdateAsync(User user);
		Task<bool> ExistsAsync(User user);
	}
}
