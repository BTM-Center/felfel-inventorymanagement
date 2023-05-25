using InventoryManagement.App.Controllers;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagement.Tests.Controllers
{
    public class SupplierControllerTests
    {
        [Fact]
        public async Task GetOrderAsync_Returns200Ok_WithTheCorrectDto()
        {
            //Arrange
            var supplierManager = new Mock<ISupplierManager>();
            supplierManager.Setup(m => m.GetOrderAsync(It.IsAny<int>())).Returns(GetTestOrderDto());

            var supplierController = new SupplierController(supplierManager.Object);

            //Act
            var result = await supplierController.GetOrderAsync(1);
            var okResult = result as OkObjectResult;
            var orderDto = okResult?.Value as OrderDto;

            //Assert
            Assert.NotNull(okResult);
            Assert.NotNull(orderDto);
            Assert.Equal(1, orderDto.Id);
        }

        [Fact]
        public async Task GetOrderAsync_Returns404NotFound()
        {
            //Arrange
            var supplierManager = new Mock<ISupplierManager>();
            supplierManager.Setup(m => m.GetOrderAsync(It.IsAny<int>())).Returns(GetNoResultDto());

            var supplierController = new SupplierController(supplierManager.Object);

            //Act
            var result = await supplierController.GetOrderAsync(1);
            var notFoundResult = result as NotFoundResult;

            //Assert
            Assert.NotNull(notFoundResult);
        }

        private Task<OrderDto> GetTestOrderDto()
        {
            return Task.Run(() =>
            {
                return new OrderDto()
                {
                    Id = 1,
                    Supplier = new SupplierDto
                    {
                        Id = 2
                    },
                    CreatedDateTime = DateTime.MinValue,
                    OrderLines = new List<OrderLineDto>()
                };
            });
        }

        private Task<OrderDto?> GetNoResultDto()
        {
            return Task.Run(() =>
            {
                return (OrderDto?)null;
            });
        }
    }
}