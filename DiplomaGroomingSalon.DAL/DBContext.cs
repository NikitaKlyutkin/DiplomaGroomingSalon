using DiplomaGroomingSalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiplomaGroomingSalon.DAL
{
	public class DBContext :DbContext
	{
		public DBContext(DbContextOptions opts) : base(opts)
		{
			Database.EnsureCreated();
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Account> Accounts { get; set; }
	}
}
