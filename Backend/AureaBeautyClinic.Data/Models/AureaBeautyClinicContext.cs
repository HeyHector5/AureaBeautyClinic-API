using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AureaBeautyClinic.Data.Models;

public partial class AureaBeautyClinicContext : DbContext
{
    public AureaBeautyClinicContext()
    {
    }

    public AureaBeautyClinicContext(DbContextOptions<AureaBeautyClinicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Specialty> Specialties { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-7H8JL71U;Database=AureaBeautyClinic;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC21AF210C3");

            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.State).HasMaxLength(50);

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Docto__440B1D61");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__UserI__4316F928");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__2DC00EBFC36B7FDD");

            entity.Property(e => e.Biography).HasMaxLength(1000);
            entity.Property(e => e.LicenseNumber).HasMaxLength(50);
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(500)
                .HasColumnName("PhotoURL");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctors__Special__403A8C7D");

            entity.HasOne(d => d.User).WithMany(p => p.Doctors)
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

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
