using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;
using Shared.Enums;

namespace InventoryManagement.Tests.Builders.Product
{
    internal class ProductBatchWithFreshnessStatusDtoBuilder
    {
        private readonly ProductBatchWithFreshnessStatusDto _productBatchWithFreshnessStatusDto;

        private ProductBatchWithFreshnessStatusDtoBuilder()
        {
            _productBatchWithFreshnessStatusDto = new ProductBatchWithFreshnessStatusDto()
            {
                ProductBatch = new ProductBatchDto()
            };
        }
        
        public static ProductBatchWithFreshnessStatusDtoBuilder Create()
        {
            return new ProductBatchWithFreshnessStatusDtoBuilder();
        }

        public ProductBatchWithFreshnessStatusDto Build()
        {
            return _productBatchWithFreshnessStatusDto;
        }

        public ProductBatchWithFreshnessStatusDtoBuilder WithProductBatch(int productBatchId)
        {
            _productBatchWithFreshnessStatusDto.ProductBatch.Id = productBatchId;
            return this;
        }

        public ProductBatchWithFreshnessStatusDtoBuilder WithFreshnessStatus(ProductBatchFreshnessStatus freshnessStatus)
        {
            _productBatchWithFreshnessStatusDto.FreshnessStatus = freshnessStatus;
            return this;
        }
    }
}
