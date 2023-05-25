using InventoryManagement.Core.Dtos.WrapperDtos;

namespace InventoryManagement.Core.Managers.Interfaces
{
    public interface ICustomerManager
    {
        Task CreateCustomerDeliveryAsync(CreateCustomerDeliveryDto createCustomerDeliveryDto);
    }
}
