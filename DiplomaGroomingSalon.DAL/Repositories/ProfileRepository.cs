using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class ProfileRepository : IBaseRepository<Profile>
	{
		private readonly DBContext _db;

		public ProfileRepository(DBContext db)
		{
			_db = db;
		}
		public async Task Create(Profile entity)
		{
			await _db.Profiles.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

		public IQueryable<Profile> GetAll()
		{
			return _db.Profiles;
		}

		public async Task Delete(Profile entity)
		{
			_db.Profiles.Remove(entity);
			await _db.SaveChangesAsync();
		}

		public async Task<Profile> Update(Profile entity)
		{
			_db.Profiles.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}

        public Task DeleteRange(Profile entity)
        {
            throw new NotImplementedException();
        }
    }
}
