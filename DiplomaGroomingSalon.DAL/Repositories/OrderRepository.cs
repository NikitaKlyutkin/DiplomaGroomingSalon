using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;

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

		public IQueryable<Order> GetAll()
		{
			return _db.Orders;
		}

		public async Task Delete(Order entity)
		{
			_db.Orders.Remove(entity);
			await _db.SaveChangesAsync();
		}

		public async Task<Order> Update(Order entity)
		{
			_db.Orders.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}
	}
}
