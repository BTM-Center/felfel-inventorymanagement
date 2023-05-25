using InventoryManagement.App.Controllers;
using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Core.Managers.Interfaces;
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
            var createCustomerDeliveryDto = new CreateCustomerDeliveryDto()
            {
                CustomerId = 1,
                ProductBatchId = 1,
                DeliveryDate = DateTime.Now,
                Units = 100
            };

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
