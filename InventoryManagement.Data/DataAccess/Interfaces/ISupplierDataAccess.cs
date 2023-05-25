using InventoryManagement.Data.Entities;

namespace InventoryManagement.Data.DataAccess.Interfaces
{
    public interface ISupplierDataAccess
    {
        Task<Order> GetOrderAsync(int id);
        Task<Order> GetOrderForProductBatchAsync(int productBatchId);
        Task<int> CreateOrderAsync(Order order);
        Task CreateOrderLineAsync(OrderLine orderLine, int orderId);
    }
}
