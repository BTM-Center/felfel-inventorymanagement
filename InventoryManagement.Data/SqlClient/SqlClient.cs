using InventoryManagement.Data.SqlClient.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using static Shared.Constants.Constants;

namespace InventoryManagement.Data.SqlClient
{
    internal class SqlClient : ISqlClient
    {
        private readonly string _connectionString;

        public SqlClient(IConfiguration configuration)
        {
            _connectionString = configuration[ApiConfiguration.DbConnectionString];
        }

        public async Task<IDbConnection> GetDbConnectionAsync(int retryCount)
        {
            try
            {
                var sqlConnection = new SqlConnection(_connectionString);
                await sqlConnection.OpenAsync();
                
                return sqlConnection;
            }
            catch
            {
                if (++retryCount == 4)
                {
                    throw;
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(retryCount));
                    return await GetDbConnectionAsync(retryCount);
                }
            }
        }
    }
}
