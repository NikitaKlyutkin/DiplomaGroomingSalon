using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class UserRepository : IAccountRepository<User>
	{
		private readonly DBContextGroomingSalon _db;

		public UserRepository(DBContextGroomingSalon db)
		{
			_db = db;
		}
		public async Task Create(User entity)
		{
			await _db.Users.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

        public async Task<IEnumerable<User>> GetAll()
        {
			return await _db.Users.ToListAsync();
		}


        public async Task<User?> GetByNameAsync(string name)
        {
            return await _db.Users.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _db.Users.SingleOrDefaultAsync(x => x.Id == id);
        }


        public async Task<User> Update(User entity)
		{
			_db.Users.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}
	}
}
