using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;

namespace InventoryManagement.Tests.Builders.Product
{
    internal class ProductBatchHistoryDtoBuilder
    {
        private readonly ProductBatchHistoryDto _productBatchHistoryDto;

        private ProductBatchHistoryDtoBuilder()
        {
            _productBatchHistoryDto = new ProductBatchHistoryDto()
            {
                Order = new OrderDto(),
                CustomerDeliveries = new List<CustomerDeliveryDto>()
            };
        }
        
        public static ProductBatchHistoryDtoBuilder Create()
        {
            return new ProductBatchHistoryDtoBuilder();
        }

        public ProductBatchHistoryDto Build()
        {
            return _productBatchHistoryDto;
        }

        public ProductBatchHistoryDtoBuilder WithOrder(int orderId)
        {
            _productBatchHistoryDto.Order.Id = orderId;

            return this;
        }

        public ProductBatchHistoryDtoBuilder WithCustomerDelivery(int customerDeliveryId)
        {
            _productBatchHistoryDto.CustomerDeliveries.Add(new CustomerDeliveryDto
            { 
                Id = customerDeliveryId
            });
            
            return this;
        }
    }
}
