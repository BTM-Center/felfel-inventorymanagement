using InventoryManagement.Core.Dtos.WrapperDtos;

namespace InventoryManagement.Tests.Builders.Customer
{
    internal class CreateCustomerDeliveryDtoBuilder
    {
        private readonly CreateCustomerDeliveryDto _createCustomerDeliveryDto;

        private CreateCustomerDeliveryDtoBuilder()
        {
            _createCustomerDeliveryDto = new CreateCustomerDeliveryDto();
        }
        
        public static CreateCustomerDeliveryDtoBuilder Create()
        {
            return new CreateCustomerDeliveryDtoBuilder();
        }

        public CreateCustomerDeliveryDto Build()
        {
            return _createCustomerDeliveryDto;
        }

        public CreateCustomerDeliveryDtoBuilder WithCustomerId(int customerId)
        {
            _createCustomerDeliveryDto.CustomerId = customerId;
            return this;
        }

        public CreateCustomerDeliveryDtoBuilder WithProductBatchId(int productBatchId)
        {
            _createCustomerDeliveryDto.ProductBatchId = productBatchId;
            return this;
        }

        public CreateCustomerDeliveryDtoBuilder WithDeliveryDate(DateTime deliveryDate)
        {
            _createCustomerDeliveryDto.DeliveryDate = deliveryDate;
            return this;
        }

        public CreateCustomerDeliveryDtoBuilder WithUnits(int units)
        {
            _createCustomerDeliveryDto.Units = units;
            return this;
        }
    }
}
