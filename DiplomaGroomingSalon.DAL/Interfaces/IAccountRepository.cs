using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.DAL.Interfaces
{
    public interface IAccountRepository<T>
    {
        Task Create(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetByNameAsync(string name);
        Task<T?> GetByIdAsync(Guid id);


        Task<T> Update(T entity);
    }
}
