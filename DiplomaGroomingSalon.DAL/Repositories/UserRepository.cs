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
	public class UserRepository : IAccountRepository<User>
	{
		private readonly DBContext _db;

		public UserRepository(DBContext db)
		{
			_db = db;
		}
		public async Task Create(User entity)
		{
			await _db.Users.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
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
