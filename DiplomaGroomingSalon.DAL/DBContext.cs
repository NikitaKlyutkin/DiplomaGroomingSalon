using DiplomaGroomingSalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Helpers;

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
		public DbSet<Profile> Profiles { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<PetType> PetTypes { get; set; }
		public DbSet<Breed> BreedPets { get; set; }
		public DbSet<ServiceType> ServiceTypes { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>(builder =>
			{
				builder.ToTable("Users").HasKey(x => x.Id);
				builder.HasData(new User[]
				{
					new User()
					{
						Id = new Guid("08027984-8ecb-4dde-a9b3-bcaec5bc7604"),
						Name = "Admin",
						Password = HashPasswordHelper.HashPassowrd("123456"),
						Role = Role.Admin
					},
				});
				builder.ToTable("Users").HasKey(x => x.Id);

				builder.Property(x => x.Id).ValueGeneratedOnAdd();

				builder.Property(x => x.Password).IsRequired();
				builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

				builder.HasOne(x => x.Profile)
					.WithOne(x => x.User)
					.HasPrincipalKey<User>(x => x.Id)
					.OnDelete(DeleteBehavior.Cascade);
			});
			modelBuilder.Entity<Profile>(builder =>
			{
				builder.ToTable("Profiles").HasKey(x => x.Id);

				builder.Property(x => x.Id).ValueGeneratedOnAdd();
				builder.Property(x => x.Name);
				builder.Property(x => x.Surname);
				builder.Property(x => x.Email);
				builder.Property(x => x.Phone);


				builder.HasData(new Profile()
				{
					Id = new Guid("08027984-8ecb-4dde-a9b3-bcaec5bc7605"),
					UserId = new Guid("08027984-8ecb-4dde-a9b3-bcaec5bc7604")
				});
			});
			modelBuilder.Entity<Order>(builder =>
			{
				builder.ToTable("Orders").HasKey(x => x.Id);

				builder.HasOne(r => r.Profile).WithMany(t => t.Orders)
					.HasForeignKey(r => r.ProfileId);
			});
		}

	}
}
