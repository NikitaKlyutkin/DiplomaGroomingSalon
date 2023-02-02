using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class TypePetRepository : IBaseRepository<TypePet>
	{
		private readonly DBContext _db;
		public TypePetRepository(DBContext db)
		{
			_db = db;
		}
		public async Task Create(TypePet entity)
		{
			await _db.TypePets.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

		public IQueryable<TypePet> GetAll()
		{
			return _db.TypePets;
		}

		public async Task Delete(TypePet entity)
		{
			_db.TypePets.Remove(entity);
			await _db.SaveChangesAsync();
		}

		public async Task<TypePet> Update(TypePet entity)
		{
			_db.TypePets.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}
	}
}
