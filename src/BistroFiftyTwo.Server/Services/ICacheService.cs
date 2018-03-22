using System.Threading.Tasks;

namespace BistroFiftyTwo.Server.Services
{
    public interface ICacheService
    {
        Task SetAsync<T>(string key, T entity, double durationMs) where T : class;
        Task<T> GetAsync<T>(string key);
    }
}