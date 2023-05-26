using InventoryManagement.App.Controllers;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Core.Managers.Interfaces;
using InventoryManagement.Tests.Builders.Supplier;
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
            var testOrderDto = OrderDtoBuilder.Create()
                .WithId(1)
                .WithSupplier(1)
                .WithCreatedDateTime(DateTime.Now)
                .Build();

            var supplierManager = new Mock<ISupplierManager>();
            supplierManager.Setup(m => m.GetOrderAsync(It.IsAny<int>())).Returns(Task.FromResult(testOrderDto));

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
            supplierManager.Setup(m => m.GetOrderAsync(It.IsAny<int>())).Returns(Task.FromResult((OrderDto?)null));

            var supplierController = new SupplierController(supplierManager.Object);

            //Act
            var result = await supplierController.GetOrderAsync(1);
            var notFoundResult = result as NotFoundResult;

            //Assert
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task CreateOrderAsync_Returns200Ok()
        {
            //Arrange
            var testCreateOrderDto = CreateOrderDtoBuilder.Create()
                .WithSupplier(1)
                .Build();

            var supplierManager = new Mock<ISupplierManager>();
            supplierManager.Setup(m => m.CreateOrderAsync(It.IsAny<CreateOrderDto>())).Returns(Task.FromResult(1));

            var supplierController = new SupplierController(supplierManager.Object);

            //Act
            var result = await supplierController.CreateOrderAsync(testCreateOrderDto);
            var createdResult = result as CreatedResult;

            //Assert
            Assert.NotNull(createdResult);
        }
    }
}