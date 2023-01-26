using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class UserRepository : IBaseRepository<User>
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

		public IQueryable<User> GetAll()
		{
			return _db.Users;
		}

		public async Task Delete(User entity)
		{
			_db.Users.Remove(entity);
			await _db.SaveChangesAsync();
		}

		public async Task<User> Update(User entity)
		{
			_db.Users.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}
	}
}
