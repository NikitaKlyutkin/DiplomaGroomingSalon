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
	public class OrderRepository : IBaseRepository<Order>
	{
		private readonly DBContext _db;

		public OrderRepository(DBContext db)
		{
			_db = db;
		}
		public async Task Create(Order entity)
		{
			await _db.Orders.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _db.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Delete(Order entity)
		{
			_db.Orders.Remove(entity);
			await _db.SaveChangesAsync();
		}

        public Task DeleteRange(Order entity)
        {
            throw new NotImplementedException();
        }


        public async Task<Order> Update(Order entity)
		{
			_db.Orders.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}
	}
}
