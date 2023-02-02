using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.DAL.Repositories;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Service.Implementations;
using DiplomaGroomingSalon.Service.Interfaces;
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
			services.AddScoped<IBaseRepository<TypePet>, TypePetRepository>();
            services.AddScoped<IBaseRepository<BreedPet>, BreedPetRepository>();
			services.AddScoped<IBaseRepository<ServiceType>, ServiceTypeRepository>();

			services.AddScoped<IAppointmentService, AppointmentService>();
			services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPriceCascadingService, PriceCascadingService>();
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
