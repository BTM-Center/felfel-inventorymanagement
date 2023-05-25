using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;

namespace InventoryManagement.Core.Managers.Interfaces
{
    public interface IProductManager
    {
        Task<ProductBatchDto> GetProductBatchAsync(int id);
        Task<IList<ProductBatchDto>> GetBatchesForProductAsync(int productId);
        Task<IList<ProductBatchWithFreshnessStatusDto>> GetAvailableProductBatchesWithFreshnessStatusAsync();
        Task<ProductBatchHistoryDto> GetProductBatchHistoryAsync(int id);
        Task<bool> UpdateProductBatchAsync(UpdateProductBatchDto updateProductBatchDto);
    }
}
