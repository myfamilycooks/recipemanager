using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BistroFiftyTwo.Server.Services
{
    public interface IEntityService<T>
    {
        Task<T> Get(Guid id);
        Task<T> Create(T item);
        Task<IEnumerable<T>> GetAll();
        Task<T> Update(T item);
        Task Delete(T item);
    }
}