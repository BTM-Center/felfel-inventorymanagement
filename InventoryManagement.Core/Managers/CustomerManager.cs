using AutoMapper;
using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Core.Managers.Interfaces;
using InventoryManagement.Data.DataAccess.Interfaces;
using InventoryManagement.Data.Entities;
using Shared.Exceptions;

namespace InventoryManagement.Core.Managers
{
    internal class CustomerManager : ICustomerManager
    {
        private readonly ICustomerDataAccess _customerDataAccess;
        private readonly IProductDataAccess _productDataAccess;
        private readonly IMapper _mapper;

        public CustomerManager(ICustomerDataAccess customerDataAccess, IProductDataAccess productDataAccess, IMapper mapper)
        {
            _customerDataAccess = customerDataAccess;
            _productDataAccess = productDataAccess;
            _mapper = mapper;
        }

        public async Task CreateCustomerDeliveryAsync(CreateCustomerDeliveryDto createCustomerDeliveryDto)
        {
            var customerDelivery = _mapper.Map<CustomerDelivery>(createCustomerDeliveryDto);

            await CheckProductBatchStockAsync(customerDelivery);
            await _customerDataAccess.CreateCustomerDeliveryAsync(customerDelivery);
            await UpdateProductBatchStockAsync(customerDelivery);
        }

        private async Task CheckProductBatchStockAsync(CustomerDelivery customerDelivery)
        {
            var productBatch = await _productDataAccess.GetProductBatchAsync(customerDelivery.ProductBatch.Id);
            if (productBatch.Units < customerDelivery.Units)
            {
                throw new NotEnoughStockException("Not enough stock in the selected batch in order to honor the delivery");
            }
        }

        private async Task UpdateProductBatchStockAsync(CustomerDelivery customerDelivery)
        {
            var productBatch = await _productDataAccess.GetProductBatchAsync(customerDelivery.ProductBatch.Id);
            productBatch.Units -= customerDelivery.Units;

            await _productDataAccess.UpdateProductBatchAsync(productBatch);
        }
    }
}
