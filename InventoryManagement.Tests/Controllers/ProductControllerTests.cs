using InventoryManagement.App.Controllers;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Core.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagement.Tests.Controllers
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task GetProductBatchAsync_Returns200Ok_WithTheCorrectDto()
        {
            //Arrange
            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetProductBatchAsync(It.IsAny<int>())).Returns(GetTestProductBatchDto());

            var productController = new ProductController(productManager.Object);

            //Act
            var result = await productController.GetProductBatchAsync(1);
            var okResult = result as OkObjectResult;
            var productBatchDto = okResult?.Value as ProductBatchDto;

            //Assert
            Assert.NotNull(okResult);
            Assert.NotNull(productBatchDto);
            Assert.Equal(1, productBatchDto.Id);
        }

        [Fact]
        public async Task GetProductBatchAsync_Returns404NotFound()
        {
            //Arrange
            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetProductBatchAsync(It.IsAny<int>())).Returns(GetNoProductBatchResultDto());

            var productController = new ProductController(productManager.Object);

            //Act
            var result = await productController.GetProductBatchAsync(1);
            var notFoundResult = result as NotFoundResult;

            //Assert
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task GetBatchesForProductAsync_Returns200Ok_WithTheCorrectDto()
        {
            //Arrange
            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetBatchesForProductAsync(It.IsAny<int>())).Returns(GetTestProductBatchesDto());

            var productController = new ProductController(productManager.Object);

            //Act
            var result = await productController.GetBatchesForProductAsync(1);
            var okResult = result as OkObjectResult;
            var productBatchDtos = okResult?.Value as IList<ProductBatchDto>;

            //Assert
            Assert.NotNull(okResult);
            Assert.NotNull(productBatchDtos);
            Assert.Equal(1, productBatchDtos[0].Id);
        }

        [Fact]
        public async Task GetBatchesForProductAsync_Returns404NotFound()
        {
            //Arrange
            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetBatchesForProductAsync(It.IsAny<int>())).Returns(GetNoProductBatchesResultDto());

            var productController = new ProductController(productManager.Object);

            //Act
            var result = await productController.GetBatchesForProductAsync(1);
            var notFoundResult = result as NotFoundResult;

            //Assert
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task GetAvailableProductBatchesWithFreshnessStatusAsync_Returns200Ok_WithTheCorrectDto()
        {
            //Arrange
            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetAvailableProductBatchesWithFreshnessStatusAsync()).Returns(GetTestProductBatchesWithFreshnessStatusDto());

            var productController = new ProductController(productManager.Object);

            //Act
            var result = await productController.GetAvailableProductBatchesWithFreshnessStatusAsync();
            var okResult = result as OkObjectResult;
            var productBatchDtos = okResult?.Value as IList<ProductBatchWithFreshnessStatusDto>;

            //Assert
            Assert.NotNull(okResult);
            Assert.NotNull(productBatchDtos);
            Assert.Equal(1, productBatchDtos[0].ProductBatch.Id);
        }

        [Fact]
        public async Task GetAvailableProductBatchesWithFreshnessStatusAsync_Returns404NotFound()
        {
            //Arrange
            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetAvailableProductBatchesWithFreshnessStatusAsync()).Returns(GetNoProductBatchesWithFreshnessStatusResultDto());

            var productController = new ProductController(productManager.Object);

            //Act
            var result = await productController.GetAvailableProductBatchesWithFreshnessStatusAsync();
            var notFoundResult = result as NotFoundResult;

            //Assert
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task GetProductBatchHistoryAsync_Returns200Ok_WithTheCorrectDto()
        {
            //Arrange
            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetProductBatchHistoryAsync(It.IsAny<int>())).Returns(GetTestProductBatchWithHistoryDto());

            var productController = new ProductController(productManager.Object);

            //Act
            var result = await productController.GetProductBatchHistoryAsync(1);
            var okResult = result as OkObjectResult;
            var productBatchHistoryDto = okResult?.Value as ProductBatchHistoryDto;

            //Assert
            Assert.NotNull(okResult);
            Assert.NotNull(productBatchHistoryDto);
            Assert.Equal(1, productBatchHistoryDto.Order.Id);
        }

        [Fact]
        public async Task GetProductBatchHistoryAsync_Returns404NotFound()
        {
            //Arrange
            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetProductBatchHistoryAsync(It.IsAny<int>())).Returns(GetNoProductBatchWithHistoryDto());

            var productController = new ProductController(productManager.Object);

            //Act
            var result = await productController.GetProductBatchHistoryAsync(1);
            var notFoundResult = result as NotFoundResult;

            //Assert
            Assert.NotNull(notFoundResult);
        }

        private Task<ProductBatchDto> GetTestProductBatchDto()
        {
            return Task.Run(() =>
            {
                return new ProductBatchDto()
                {
                    Id = 1
                };
            });
        }

        private Task<IList<ProductBatchDto>> GetTestProductBatchesDto()
        {
            return Task.Run(() =>
            {
                IList<ProductBatchDto> productBatches = new List<ProductBatchDto>()
                {
                    new ProductBatchDto() { Id = 1 }
                };

                return productBatches;
            });
        }

        private Task<IList<ProductBatchWithFreshnessStatusDto>> GetTestProductBatchesWithFreshnessStatusDto()
        {
            return Task.Run(() =>
            {
                IList<ProductBatchWithFreshnessStatusDto> productBatchesWithFreshnessStatus = new List<ProductBatchWithFreshnessStatusDto>()
                {
                    new ProductBatchWithFreshnessStatusDto() 
                    { 
                        ProductBatch = new ProductBatchDto()
                        {
                            Id = 1
                        }
                    }
                };

                return productBatchesWithFreshnessStatus;
            });
        }

        private Task<ProductBatchHistoryDto> GetTestProductBatchWithHistoryDto()
        {
            return Task.Run(() =>
            {
                return new ProductBatchHistoryDto
                { 
                    Order = new OrderDto()
                    {
                        Id = 1
                    }
                };
            });
        }

        private Task<ProductBatchDto?> GetNoProductBatchResultDto()
        {
            return Task.Run(() =>
            {
                return (ProductBatchDto?)null;
            });
        }

        private Task<IList<ProductBatchDto>?> GetNoProductBatchesResultDto()
        {
            return Task.Run(() =>
            {
                return (IList<ProductBatchDto>?)null;
            });
        }

        private Task<IList<ProductBatchWithFreshnessStatusDto>?> GetNoProductBatchesWithFreshnessStatusResultDto()
        {
            return Task.Run(() =>
            {
                return (IList<ProductBatchWithFreshnessStatusDto>?)null;
            });
        }

        private Task<ProductBatchHistoryDto?> GetNoProductBatchWithHistoryDto()
        {
            return Task.Run(() =>
            {
                return (ProductBatchHistoryDto?)null;
            });
        }
    }
}
