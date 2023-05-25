using System.Data;

namespace InventoryManagement.Data.SqlClient.Interfaces
{
    public interface ISqlClient
    {
        Task<IDbConnection> GetDbConnectionAsync(int retryCount = 0);
    }
}
