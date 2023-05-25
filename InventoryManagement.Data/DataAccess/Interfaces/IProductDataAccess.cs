using InventoryManagement.Data.Entities;
using Shared.Enums;

namespace InventoryManagement.Data.DataAccess.Interfaces
{
    public interface IProductDataAccess
    {
        Task<ProductBatch> GetProductBatchAsync(int id);
        Task<IList<ProductBatch>> GetProductBatchesForProductAsync(int productId);
        Task<IList<(ProductBatch, ProductBatchFreshnessStatus)>> GetAvailableProductBatchesWithFreshnessStatus();
        Task<int> CreateProductBatchAsync(ProductBatch productBatch);
        Task<bool> UpdateProductBatchAsync(ProductBatch productBatch);
    }
}
