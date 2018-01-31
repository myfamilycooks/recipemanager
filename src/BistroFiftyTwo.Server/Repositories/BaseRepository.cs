using System.Threading.Tasks;
using BistroFiftyTwo.Server.Services;
using Npgsql;

namespace BistroFiftyTwo.Server.Repositories
{
    public class BaseRepository
    {
        protected IConfigurationService ConfigurationService { get; set; }

        protected async Task<NpgsqlConnection> CreateConnection()
        {
            var connection = new NpgsqlConnection(ConfigurationService.Get("Data:RecipeX:ConnectionString"));
            await connection.OpenAsync();
            return connection;
        }

        protected NpgsqlConnection CreateConnectionSynchronous()
        {
            var connection = new NpgsqlConnection(ConfigurationService.Get("Data:RecipeX:ConnectionString"));
            connection.Open();
            return connection;
        }
    }
}