using InventoryManagement.App.Controllers;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Core.Managers.Interfaces;
using InventoryManagement.Tests.Builders.Product;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Enums;
using Xunit;

namespace InventoryManagement.Tests.Controllers
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task GetProductBatchAsync_Returns200Ok_WithTheCorrectDto()
        {
            //Arrange
            var testProductBatchDto = ProductBatchDtoBuilder.Create()
                .WithId(1)
                .WithProduct(1)
                .WithUnits(100)
                .Build();

            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetProductBatchAsync(It.IsAny<int>())).Returns(Task.FromResult(testProductBatchDto));

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
            productManager.Setup(m => m.GetProductBatchAsync(It.IsAny<int>())).Returns(Task.FromResult((ProductBatchDto?)null));

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
            var testProductBatchDto = ProductBatchDtoBuilder.Create()
                .WithId(1)
                .WithProduct(1)
                .WithUnits(100)
                .Build();

            IList<ProductBatchDto> testProductBatchesDto = new List<ProductBatchDto>()
            {
                testProductBatchDto
            };

            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetBatchesForProductAsync(It.IsAny<int>())).Returns(Task.FromResult(testProductBatchesDto));

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
            productManager.Setup(m => m.GetBatchesForProductAsync(It.IsAny<int>())).Returns(Task.FromResult((IList<ProductBatchDto>?)null));

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
            var productBatchWithFreshnessStatus = ProductBatchWithFreshnessStatusDtoBuilder.Create()
                .WithProductBatch(1)
                .WithFreshnessStatus(ProductBatchFreshnessStatus.Fresh)
                .Build();

            IList<ProductBatchWithFreshnessStatusDto> productBatchesWithFreshnessStatus = new List<ProductBatchWithFreshnessStatusDto>()
            {
                productBatchWithFreshnessStatus
            };

            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetAvailableProductBatchesWithFreshnessStatusAsync()).Returns(Task.FromResult(productBatchesWithFreshnessStatus));

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
            productManager.Setup(m => m.GetAvailableProductBatchesWithFreshnessStatusAsync()).Returns(Task.FromResult((IList<ProductBatchWithFreshnessStatusDto>?)null));

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
            var testProductBatchHistoryDto = ProductBatchHistoryDtoBuilder.Create()
                .WithOrder(1)
                .WithCustomerDelivery(1)
                .Build();

            var productManager = new Mock<IProductManager>();
            productManager.Setup(m => m.GetProductBatchHistoryAsync(It.IsAny<int>())).Returns(Task.FromResult(testProductBatchHistoryDto));

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
            productManager.Setup(m => m.GetProductBatchHistoryAsync(It.IsAny<int>())).Returns(Task.FromResult((ProductBatchHistoryDto?)null));

            var productController = new ProductController(productManager.Object);

            //Act
            var result = await productController.GetProductBatchHistoryAsync(1);
            var notFoundResult = result as NotFoundResult;

            //Assert
            Assert.NotNull(notFoundResult);
        }
    }
}
