using InventoryManagement.Data.DataAccess;
using InventoryManagement.Data.DataAccess.Interfaces;
using InventoryManagement.Data.SqlClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Data
{
    public static class DataContainer
    {
        public static void AddDataDependencyInversion(this IServiceCollection services)
        {
            services.AddScoped<ICustomerDataAccess, CustomerDataAccess>();
            services.AddScoped<IProductDataAccess, ProductDataAccess>();
            services.AddScoped<ISupplierDataAccess, SupplierDataAccess>();

            services.AddSingleton<ISqlClient, SqlClient.SqlClient>();
        }
    }
}
