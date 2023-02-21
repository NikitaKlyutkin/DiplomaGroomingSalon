using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class PetTypeRepository : IBaseRepository<PetType>
	{
		private readonly DBContext _db;
		public PetTypeRepository(DBContext db)
		{
			_db = db;
		}
		public async Task Create(PetType entity)
		{
			await _db.PetTypes.AddAsync(entity);
			await _db.SaveChangesAsync();
		}
        private IQueryable<PetType> GetAllPrivate()
        {
            return _db.PetTypes;
        }
        public async Task<IEnumerable<PetType>> GetAll()
        {
            var type = GetAllPrivate();
            return await type.ToListAsync();
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
