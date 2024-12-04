using Dapper;
using DataMseAPI.Model;
using DataMseAPI.Models;
using DataMseAPI.Repository.Interface;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Runtime;

namespace DataMseAPI.Repository
{
    public class MseDataRepository : IMseDataRepository
    {
        private readonly DbSettings _dbSettings;
        public MseDataRepository(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        public async Task<IEnumerable<MseData>> GetAllDataAsync()
        {
            using (var connection = new NpgsqlConnection(_dbSettings.PostgreSQL))
            {
                await connection.OpenAsync();
                string sql = "SELECT * FROM MseData";

                var result = await connection.QueryAsync<MseData>(sql);

                return result;
            }
        }
        
        public async Task<IEnumerable<String>> GetCodesAsync()
        {
            using (var connection = new NpgsqlConnection(_dbSettings.PostgreSQL))
            {
                await connection.OpenAsync();
                string sql = "SELECT DISTINCT Code FROM MseData";

                var result = await connection.QueryAsync<String>(sql);

                return result;
            }
        }

        public async Task<IEnumerable<MseData>> GetDataByCodeAsync(String code)
        {
            using (var connection = new NpgsqlConnection(_dbSettings.PostgreSQL))
            {
                await connection.OpenAsync();
                string sql = "SELECT * FROM MseData WHERE code = @code";

                var result = await connection.QueryAsync<MseData>(sql, new { code });

                return result;
            }
        }
    }
}
