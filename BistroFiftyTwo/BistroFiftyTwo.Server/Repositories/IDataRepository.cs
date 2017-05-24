using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BistroFiftyTwo.Server.Repositories
{
    public interface IDataRepository<T>
    {
        Task<T> GetAsync(Guid id);
        Task<T> CreateAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> UpdateAsync(T item);
        Task DeleteAsync(T item);
    }
}