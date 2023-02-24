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
	public class ProfileRepository : IAccountRepository<Profile>
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

        public async Task<IEnumerable<Profile>> GetAll()
        {
            return await _db.Profiles.ToListAsync();
        }

        public async Task<Profile?> GetByNameAsync(string name)
        {
            return await _db.Profiles.SingleOrDefaultAsync(x => x.User.Name == name);
        }

        public async Task<Profile> Update(Profile entity)
		{
			_db.Profiles.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}


        public async Task<Profile?> GetByIdAsync(Guid id)
        {
            return await _db.Profiles.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
