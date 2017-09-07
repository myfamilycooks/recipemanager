using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroFiftyTwo.Server.Entities;
using BistroFiftyTwo.Server.Services;
using Npgsql;
using Dapper;

namespace BistroFiftyTwo.Server.Repositories
{
    public class AutomaticDataRepository<T> : IDataRepository<T> where T : IBistroEntity, new()
    {
        public AutomaticDataRepository(IConfigurationService configurationService)
        {
            Connection = new NpgsqlConnection(configurationService.Get("Data:Recipe:ConnectionString"));
            Connection.Open();
        }

        protected NpgsqlConnection Connection { get; set; }

        public async Task<T> GetAsync(Guid id)
        {
            var ent = new T();
            var cols = ent.Columns().ToArray();

            var columnList = string.Join(", ", cols);
            var sql = $"select {columnList} from {ent.TableName()} where id = @id";

            return await Connection.QuerySingleAsync<T>(sql, new {id});
        }

        public async Task<T> CreateAsync(T item)
        {
            var cols = item.Columns().Where(c => c != "id");
            var paramCols = cols.Select(c => $"@{c}");

            var sql =
                $"insert into {item.TableName()} ({string.Join(",", cols)}) values ({string.Join(", ", paramCols)}) returning *";

            var dp = new DynamicParameters();

            var itemDict = item.ToDictionary();

            foreach (var pair in itemDict)
                dp.Add(pair.Key, pair.Value);

            return await Connection.QuerySingleAsync<T>(sql, dp);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}