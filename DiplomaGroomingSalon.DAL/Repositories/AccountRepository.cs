using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class AccountRepository : IBaseRepository<Account>
	{
		private readonly DBContext _db;

		public AccountRepository(DBContext db)
		{
			_db = db;
		}
		public async Task Create(Account entity)
		{
			await _db.Accounts.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

		public IQueryable<Account> GetAll()
		{
			return _db.Accounts;
		}

		public async Task Delete(Account entity)
		{
			_db.Accounts.Remove(entity);
			await _db.SaveChangesAsync();
		}

		public async Task<Account> Update(Account entity)
		{
			_db.Accounts.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}

        public Task DeleteRange(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
