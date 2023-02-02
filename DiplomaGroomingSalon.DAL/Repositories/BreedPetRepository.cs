using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;

namespace DiplomaGroomingSalon.DAL.Repositories
{
	public class BreedPetRepository : IBaseRepository<BreedPet>
	{
		private readonly DBContext _db;
		public BreedPetRepository(DBContext db)
		{
			_db = db;
		}
		public async Task Create(BreedPet entity)
		{
			await _db.BreedPets.AddAsync(entity);
			await _db.SaveChangesAsync();
		}

		public IQueryable<BreedPet> GetAll()
		{
			return _db.BreedPets;
		}

		public async Task Delete(BreedPet entity)
		{
			_db.BreedPets.Remove(entity);
			await _db.SaveChangesAsync();
		}

		public async Task<BreedPet> Update(BreedPet entity)
		{
			_db.BreedPets.Update(entity);
			await _db.SaveChangesAsync();

			return entity;
		}
	}
}
