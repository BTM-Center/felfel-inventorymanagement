using InventoryManagement.Core.Dtos.WrapperDtos;

namespace InventoryManagement.Tests.Builders.Supplier
{
    internal class CreateOrderDtoBuilder
    {
        private readonly CreateOrderDto _createOrderDto;

        private CreateOrderDtoBuilder()
        {
            _createOrderDto = new CreateOrderDto()
            {
                OrderLines = new List<CreateOrderLineDto>()
            };
        }
        
        public static CreateOrderDtoBuilder Create()
        {
            return new CreateOrderDtoBuilder();
        }

        public CreateOrderDto Build()
        {
            return _createOrderDto;
        }

        public CreateOrderDtoBuilder WithSupplier(int supplierId)
        {
            _createOrderDto.SupplierId = supplierId;
            return this;
        }
    }
}
