using InventoryManagement.App.Controllers;
using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Core.Managers.Interfaces;
using InventoryManagement.Tests.Builders.Customer;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagement.Tests.Controllers
{
    public class CustomerControllerTests
    {
        [Fact]
        public async Task CreateCustomerDeliveryAsync_Returns200Ok()
        {
            //Arrange
            var createCustomerDeliveryDto = CreateCustomerDeliveryDtoBuilder.Create()
                .WithCustomerId(1)
                .WithProductBatchId(1)
                .WithDeliveryDate(DateTime.Now)
                .WithUnits(200)
                .Build();

            var customerManager = new Mock<ICustomerManager>();
            customerManager.Setup(m => m.CreateCustomerDeliveryAsync(It.IsAny<CreateCustomerDeliveryDto>()));

            var customerController = new CustomerController(customerManager.Object);

            //Act
            var result = await customerController.CreateCustomerDeliveryAsync(createCustomerDeliveryDto);
            var okResult = result as OkResult;

            //Assert
            Assert.NotNull(okResult);
        }
    }
}
