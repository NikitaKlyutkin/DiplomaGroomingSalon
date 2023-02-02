using DiplomaGroomingSalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DiplomaGroomingSalon.DAL
{
	public class DBContext :DbContext
	{
		public DBContext(DbContextOptions opts) : base(opts)
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<TypePet> TypePets { get; set; }
		public DbSet<BreedPet> BreedPets { get; set; }
		public DbSet<ServiceType> ServiceTypes { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

            modelBuilder.Entity<User>(builder =>
			{
				builder.ToTable("Users").HasKey(x => x.Id);

				builder.Property(x => x.Id).ValueGeneratedOnAdd();

				builder.Property(x => x.Password).IsRequired();
				builder.Property(x => x.Login).HasMaxLength(100).IsRequired();

				builder.HasOne(x => x.Account)
					.WithOne(x => x.User)
					.HasPrincipalKey<User>(x => x.Id)
					.OnDelete(DeleteBehavior.Cascade);
			});
		}

	}
}
