using AureaBeautyClinic.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace AureaBeautyClinic.Data.Contexts
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext() { }
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Specialty> Specialties { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Appointment>(entity =>
			{
				entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC21AF210C3");

				entity.Property(e => e.Notes).HasMaxLength(500);
				entity.Property(e => e.State).HasMaxLength(50);

				entity.HasOne(d => d.Doctor).WithMany()
					.HasForeignKey(d => d.DoctorId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Appointme__Docto__440B1D61");

				entity.HasOne(d => d.User).WithMany()
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Appointme__UserI__4316F928");
			});

			modelBuilder.Entity<Doctor>(entity =>
			{
				entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__2DC00EBFC36B7FDD");

				entity.Property(e => e.Biography).HasMaxLength(1000);
				entity.Property(e => e.LicenseNumber).HasMaxLength(50);
				entity.Property(e => e.PhotoURL)
					.HasMaxLength(500)
					.HasColumnName("PhotoURL");

				entity.HasOne(d => d.Specialty).WithMany()
					.HasForeignKey(d => d.SpecialtyId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Doctors__Special__403A8C7D");

				entity.HasOne(d => d.User).WithMany()
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Doctors__UserId__3F466844");
			});

			modelBuilder.Entity<Role>(entity =>
			{
				entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A0B09A82C");

				entity.Property(e => e.Description).HasMaxLength(500);
				entity.Property(e => e.Name).HasMaxLength(100);
			});

			modelBuilder.Entity<Specialty>(entity =>
			{
				entity.HasKey(e => e.SpecialtyId).HasName("PK__Specialt__D768F6A8F76E010D");

				entity.Property(e => e.Description).HasMaxLength(500);
				entity.Property(e => e.Name).HasMaxLength(100);
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CA07F1EFC");

				entity.HasIndex(e => e.Email, "UQ__Users__A9D105348FF4F1BC").IsUnique();

				entity.Property(e => e.Email).HasMaxLength(256);
				entity.Property(e => e.FirstName).HasMaxLength(100);
				entity.Property(e => e.LastName).HasMaxLength(100);
				entity.Property(e => e.PasswordHash).HasMaxLength(255);
				entity.Property(e => e.Phone).HasMaxLength(30);

				entity.HasOne(d => d.Role).WithMany()
					.HasForeignKey(d => d.RoleId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK__Users__RoleId__3C69FB99");
			});
		}
	}
}
