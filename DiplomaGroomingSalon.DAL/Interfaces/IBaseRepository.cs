﻿using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.DAL.Interfaces
{
	public interface IBaseRepository<T>
	{
		Task Create(T entity);

        Task<IEnumerable<T>> GetAll();
        Task<T?> GetByIdAsync(Guid id);

        Task Delete(T entity);
        Task DeleteRange(T entity);
        Task<T> Update(T entity);
	}
}
