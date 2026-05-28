using AureaBeautyClinic.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace AureaBeautyClinic.Data.Contexts
{
	public class ApplicationContext : DbContext
	{
		protected ApplicationContext() { }
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

		public DbSet<Appointments> Appointments { get; set; }
		public DbSet<Doctors> Doctors { get; set; }
		public DbSet<Roles> Roles { get; set; }
		public DbSet<Specialties> Specialties { get; set; }
		public DbSet<Users> Users { get; set; }
	}
}
