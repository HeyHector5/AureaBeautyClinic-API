using AureaBeautyClinic.Shared.Entities;

namespace AureaBeautyClinic.Shared.Interfaces.IRepositories
{
	public interface IUserRepository
	{
		Task<IEnumerable<Users>> GetAllAsync();
		Task<Users?> GetByIdAsync(int userId);
		Task<Users> CreateUserAsync(Users user);
		Task UpdateUserAsync(int userId);
		Task UpdateAsync(Users user);
		Task<bool> ExistsAsync(Users user);
	}
}
