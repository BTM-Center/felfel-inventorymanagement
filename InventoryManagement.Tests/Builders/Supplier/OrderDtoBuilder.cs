using InventoryManagement.Core.Dtos;

namespace InventoryManagement.Tests.Builders.Supplier
{
    internal class OrderDtoBuilder
    {
        private readonly OrderDto _orderDto;

        private OrderDtoBuilder()
        {
            _orderDto = new OrderDto()
            {
                Supplier = new SupplierDto()
            };
        }
        
        public static OrderDtoBuilder Create()
        {
            return new OrderDtoBuilder();
        }

        public OrderDto Build()
        {
            return _orderDto;
        }

        public OrderDtoBuilder WithId(int id)
        {
            _orderDto.Id = id;
            return this;
        }

        public OrderDtoBuilder WithSupplier(int supplierId)
        {
            _orderDto.Supplier.Id = supplierId;
            return this;
        }

        public OrderDtoBuilder WithCreatedDateTime(DateTime createdDateTime)
        {
            _orderDto.CreatedDateTime = createdDateTime;
            return this;
        }
    }
}
