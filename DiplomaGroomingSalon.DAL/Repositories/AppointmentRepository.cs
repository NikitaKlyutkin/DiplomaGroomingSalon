using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _db.Appointments.ToListAsync();
        }
        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _db.Appointments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Delete(Appointment entity)
		{
			_db.Appointments.Remove(entity);
			await _db.SaveChangesAsync();
		}

        public void DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRange(Appointment entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Appointment> Update(Appointment entity)
		{
			_db.Appointments.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}
	}
}
