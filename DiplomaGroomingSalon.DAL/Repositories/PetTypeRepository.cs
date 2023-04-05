using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class PetTypeRepository : IBaseRepository<PetType>
	{
		private readonly DBContextGroomingSalon _db;
		public PetTypeRepository(DBContextGroomingSalon db)
		{
			_db = db;
		}
		public async Task Create(PetType entity)
		{
			await _db.PetTypes.AddAsync(entity);
			await _db.SaveChangesAsync();
		}
        public async Task<IEnumerable<PetType>> GetAll()
        {
            return await _db.PetTypes.ToListAsync();
        }
        public async Task<PetType?> GetByIdAsync(Guid id)
        {
            return await _db.PetTypes.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task Delete(PetType entity)
		{
			_db.PetTypes.Remove(entity);
			await _db.SaveChangesAsync();
		}

        public Task DeleteRange(PetType entity)
        {
            throw new NotImplementedException();
        }

        public async Task<PetType> Update(PetType entity)
		{
			_db.PetTypes.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}
	}
}
