using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Entities.Interfaces;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;

namespace DiplomaGroomingSalon.Service.Interfaces
{
    public interface ICRUDDataService<T> where T : IEntity
    {
        Task<IBaseResponse<List<T>>> GetAll();
        Task<IBaseResponse<T>> GetById(Guid id);
        Task<IBaseResponse<T>> Create(T model);
        Task<IBaseResponse<T>> Edit(Guid id, T model);
        Task<IBaseResponse<bool>> Delete(Guid id);

    }
}
