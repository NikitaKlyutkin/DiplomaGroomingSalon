using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class BreedRepository : IBaseRepository<Breed>
	{
		private readonly DBContext _db;
		public BreedRepository(DBContext db)
		{
			_db = db;
		}
		public async Task Create(Breed entity)
		{
			await _db.Breeds.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

        public async Task<IEnumerable<Breed>> GetAll()
        {
            return await _db.Breeds.Include(x=>x.PetType).ToListAsync();
        }
        public async Task<Breed?> GetByIdAsync(Guid id)
        {
            return await _db.Breeds.Include(x=>x.PetType).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Delete(Breed entity)
		{
			_db.Breeds.Remove(entity);
			await _db.SaveChangesAsync();
		}

        public async Task DeleteRange(Breed entity)
        {
            _db.Breeds.RemoveRange(entity);
            await _db.SaveChangesAsync();
        }


        public async Task<Breed> Update(Breed entity)
		{
			_db.Breeds.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}
	}
}
