using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManager.WebApp.Data
{
    public interface IDataRepository<T>
    {
        Task<T> Create(T item);
        Task<IEnumerable<T>> GetAll();
        Task<T> Update(T item);
        Task<T> Get(Guid id);
        Task Delete(T item);
    }
}
