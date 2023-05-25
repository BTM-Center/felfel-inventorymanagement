using AutoMapper;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Core.Managers.Interfaces;
using InventoryManagement.Data.DataAccess.Interfaces;
using InventoryManagement.Data.Entities;

namespace InventoryManagement.Core.Managers
{
    internal class ProductManager : IProductManager
    {
        private readonly ISupplierDataAccess _supplierDataAccess;
        private readonly ICustomerDataAccess _customerDataAccess;
        private readonly IProductDataAccess _productDataAccess;
        private readonly IMapper _mapper;

        public ProductManager(ISupplierDataAccess supplierDataAccess, ICustomerDataAccess customerDataAccess, IProductDataAccess productDataAccess, IMapper mapper)
        {
            _supplierDataAccess = supplierDataAccess;
            _customerDataAccess = customerDataAccess;
            _productDataAccess = productDataAccess;
            _mapper = mapper;
        }

        public async Task<ProductBatchDto> GetProductBatchAsync(int id)
        {
            var productBatch = await _productDataAccess.GetProductBatchAsync(id);
            return _mapper.Map<ProductBatchDto>(productBatch);
        }

        public async Task<IList<ProductBatchDto>> GetBatchesForProductAsync(int productId)
        {
            var productBatches = await _productDataAccess.GetProductBatchesForProductAsync(productId);
            return _mapper.Map<IList<ProductBatchDto>>(productBatches);
        }

        public async Task<IList<ProductBatchWithFreshnessStatusDto>> GetAvailableProductBatchesWithFreshnessStatusAsync()
        {
            var productBatchesWithFreshnessStatus = await _productDataAccess.GetAvailableProductBatchesWithFreshnessStatus();
            return _mapper.Map<IList<ProductBatchWithFreshnessStatusDto>>(productBatchesWithFreshnessStatus);
        }

        public async Task<ProductBatchHistoryDto> GetProductBatchHistoryAsync(int id)
        {
            var supplierOrder = await _supplierDataAccess.GetOrderForProductBatchAsync(id);
            var customerDeliveries = await _customerDataAccess.GetCustomerDeliveriesForProductBatchAsync(id);
            return _mapper.Map<ProductBatchHistoryDto>((supplierOrder, customerDeliveries));
        }

        public async Task<bool> UpdateProductBatchAsync(UpdateProductBatchDto updateProductBatchDto)
        {
            var productBatch = _mapper.Map<ProductBatch>(updateProductBatchDto);
            return await _productDataAccess.UpdateProductBatchAsync(productBatch);
        }
    }
}
