using AutoMapper;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Core.Managers.Interfaces;
using InventoryManagement.Data.DataAccess.Interfaces;
using InventoryManagement.Data.Entities;

namespace InventoryManagement.Core.Managers
{
    internal class SupplierManager : ISupplierManager
    {
        private readonly ISupplierDataAccess _supplierDataAccess;
        private readonly IProductDataAccess _productDataAccess;
        private readonly IMapper _mapper;

        public SupplierManager(ISupplierDataAccess supplierDataAccess, IProductDataAccess productDataAccess, IMapper mapper)
        {
            _supplierDataAccess = supplierDataAccess;
            _productDataAccess = productDataAccess;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            var order = await _supplierDataAccess.GetOrderAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<int> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var order = _mapper.Map<Order>(createOrderDto);
            order.Id = await _supplierDataAccess.CreateOrderAsync(order);

            foreach (var orderLine in order.OrderLines)
            {
                await CreateOrderLineAsync(orderLine, order.Id);
            }

            return order.Id;
        }

        private async Task CreateOrderLineAsync(OrderLine orderLine, int orderId)
        {
            var productBatch = new ProductBatch()
            {
                Product = orderLine.Product,
                CheckInDate = null,
                Units = orderLine.Units
            };

            productBatch.Id = await _productDataAccess.CreateProductBatchAsync(productBatch);
            
            orderLine.ProductBatch = productBatch;
            await _supplierDataAccess.CreateOrderLineAsync(orderLine, orderId);
        }
    }
}
