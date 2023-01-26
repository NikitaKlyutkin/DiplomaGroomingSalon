using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class AppointmentRepository : IBaseRepository<Appointment>
	{
		private readonly DBContext _db;

		public AppointmentRepository(DBContext db)
		{
			_db = db;
		}
		public async Task Create(Appointment entity)
		{
			await _db.Appointments.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

		public IQueryable<Appointment> GetAll()
		{
			return _db.Appointments;
		}

		public async Task Delete(Appointment entity)
		{
			_db.Appointments.Remove(entity);
			await _db.SaveChangesAsync();
		}

		public async Task<Appointment> Update(Appointment entity)
		{
			_db.Appointments.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}
	}
}
