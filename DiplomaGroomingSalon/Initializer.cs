using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.DAL.Repositories;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Service.Implementations;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiplomaGroomingSalon;

public static class Initializer
{
	public static void InitializeRepositories(this IServiceCollection services)
	{
		services.AddScoped<IBaseRepository<Appointment>, AppointmentRepository>();
		services.AddScoped<IBaseRepository<Order>, OrderRepository>();
		services.AddScoped<IBaseRepository<PetType>, PetTypeRepository>();
		services.AddScoped<IBaseRepository<Breed>, BreedRepository>();
		services.AddScoped<IBaseRepository<ServiceType>, ServiceTypeRepository>();
		services.AddScoped<IAccountRepository<User>, UserRepository>();
		services.AddScoped<IAccountRepository<Profile>, ProfileRepository>();
	}

	public static void InitializeServices(this IServiceCollection services)
	{
		services.AddScoped<IAppointmentService, AppointmentService>();
		services.AddScoped<IOrderService, OrderService>();
		services.AddScoped<ICRUDDataService<PetType>, PetTypeService>();
		services.AddScoped<ICRUDDataService<Breed>, BreedService>();
		services.AddScoped<ICRUDDataService<ServiceType>, ServiceTypeService>();
		services.AddScoped<IAccountService, AccountService>();
		services.AddScoped<IProfileService, ProfileService>();
	}
}