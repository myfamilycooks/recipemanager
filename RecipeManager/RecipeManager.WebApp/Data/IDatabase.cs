using System.Threading.Tasks;
using Npgsql;

namespace RecipeManager.WebApp.Services
{
    public interface IDatabase
    {
        NpgsqlConnection Connection { get; set; }
        Task Connect();
    }
}