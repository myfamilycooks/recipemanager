using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace RecipeManager.WebApp.Services
{
    public class Database : IDatabase
    {
        private string ConnectionString { get; set; }

        public Database(string connectionString)
        {
            ConnectionString = connectionString;            
        }

        public async Task Connect()
        {
            if(Connection == null || Connection.State != ConnectionState.Open)
                Connection = new NpgsqlConnection(ConnectionString);

            await Connection.OpenAsync();
        }

        public NpgsqlConnection Connection { get; set; }
    }
}
