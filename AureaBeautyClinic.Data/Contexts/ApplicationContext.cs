using AureaBeautyClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AureaBeautyClinic.Data.Contexts
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Appointments> Appointments; 
		public DbSet<Doctors> Doctors; 
		public DbSet<Roles> Roles; 
		public DbSet<Specialties> Specialties; 
		public DbSet<Users> Users;
	}
}
