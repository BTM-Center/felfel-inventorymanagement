using InventoryManagement.Core.Dtos;

namespace InventoryManagement.Tests.Builders.Product
{
    internal class ProductBatchDtoBuilder
    {
        private readonly ProductBatchDto _productBatchDto;

        private ProductBatchDtoBuilder()
        {
            _productBatchDto = new ProductBatchDto()
            {
                Product = new ProductDto()
            };
        }
        
        public static ProductBatchDtoBuilder Create()
        {
            return new ProductBatchDtoBuilder();
        }

        public ProductBatchDto Build()
        {
            return _productBatchDto;
        }

        public ProductBatchDtoBuilder WithId(int id)
        {
            _productBatchDto.Id = id;
            return this;
        }

        public ProductBatchDtoBuilder WithProduct(int productId)
        {
            _productBatchDto.Product.Id = productId;
            return this;
        }

        public ProductBatchDtoBuilder WithCheckInDate(DateTime checkIdDate)
        {
            _productBatchDto.CheckInDate = checkIdDate;
            return this;
        }

        public ProductBatchDtoBuilder WithUnits(int units)
        {
            _productBatchDto.Units = units;
            return this;
        }
    }
}
