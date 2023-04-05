using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class ServiceTypeRepository : IBaseRepository<ServiceType>
	{
		private readonly DBContextGroomingSalon _db;
		public ServiceTypeRepository(DBContextGroomingSalon db)
		{
			_db = db;
		}
		public async Task Create(ServiceType entity)
		{
			await _db.ServiceTypes.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

        public async Task<IEnumerable<ServiceType>> GetAll()
        {
            return await _db.ServiceTypes.Include(x=>x.Breed.PetType).ToListAsync();
        }
        public async Task<ServiceType?> GetByIdAsync(Guid id)
        {
            return await _db.ServiceTypes.Include(x => x.Breed.PetType).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Delete(ServiceType entity)
		{
			_db.ServiceTypes.Remove(entity);
			await _db.SaveChangesAsync();
		}

        public async Task DeleteRange(ServiceType entity)
        {
            _db.ServiceTypes.RemoveRange(entity);
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
