using InventoryManagement.Data.Entities;

namespace InventoryManagement.Data.DataAccess.Interfaces
{
    public interface ICustomerDataAccess
    {
        Task<IList<CustomerDelivery>> GetCustomerDeliveriesForProductBatchAsync(int productBatchId);
        Task<bool> CreateCustomerDeliveryAsync(CustomerDelivery customerDelivery);
    }
}
