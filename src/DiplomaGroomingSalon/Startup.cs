using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DiplomaGroomingSalon.DAL;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.DAL.Repositories;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Service.Implementations;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace DiplomaGroomingSalon
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			var connectionStr = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<DBContext>(opts => opts.UseSqlServer(connectionStr));
			services.AddScoped<IBaseRepository<Appointment>, AppointmentRepository>();
			services.AddScoped<IBaseRepository<Order>, OrderRepository>();
			services.AddScoped<IBaseRepository<PetType>, PetTypeRepository>();
            services.AddScoped<IBaseRepository<Breed>, BreedRepository>();
			services.AddScoped<IBaseRepository<ServiceType>, ServiceTypeRepository>();
            services.AddScoped<IAccountRepository<User>, UserRepository>();
            services.AddScoped<IAccountRepository<Profile>, ProfileRepository>();

            services.AddScoped<IAppointmentService, AppointmentService>();
			services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICRUDDataService<PetType>, PetTypeService>();
            services.AddScoped<ICRUDDataService<Breed>, BreedService>();
            services.AddScoped<ICRUDDataService<ServiceType>, ServiceTypeService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});			
		}
	}
}
