using InventoryManagement.Core.Mapping;
using InventoryManagement.Core.Managers.Interfaces;
using InventoryManagement.Core.Managers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InventoryManagement.Core
{
    public static class CoreContainer
    {
        public static void AddCoreDependencyInversion(this IServiceCollection services)
        {
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<ISupplierManager, SupplierManager>();
        }

        public static void AddCoreAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);
        }
    }
}
