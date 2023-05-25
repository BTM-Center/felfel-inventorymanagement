using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;

namespace InventoryManagement.Core.Managers.Interfaces
{
    public interface ISupplierManager
    {
        Task<OrderDto> GetOrderAsync(int id);
        Task<int> CreateOrderAsync(CreateOrderDto createOrderDto);
    }
}
