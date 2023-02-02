using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class ServiceTypeRepository : IBaseRepository<ServiceType>
	{
		private readonly DBContext _db;
		public ServiceTypeRepository(DBContext db)
		{
			_db = db;
		}
		public async Task Create(ServiceType entity)
		{
			await _db.ServiceTypes.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

		public IQueryable<ServiceType> GetAll()
		{
			return _db.ServiceTypes;
		}

		public async Task Delete(ServiceType entity)
		{
			_db.ServiceTypes.Remove(entity);
			await _db.SaveChangesAsync();
		}

		public async Task<ServiceType> Update(ServiceType entity)
		{
			_db.ServiceTypes.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}
	}
}
