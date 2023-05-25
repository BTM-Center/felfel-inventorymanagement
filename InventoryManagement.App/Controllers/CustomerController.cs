using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Core.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Shared.Constants.Constants;

namespace InventoryManagement.App.Controllers
{
    [ApiController]
    [Route(ApiRoutes.ControllerName)]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        [HttpPost(ApiRoutes.CreateCustomerDelivery)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomerDeliveryAsync(CreateCustomerDeliveryDto createCustomerDeliveryDto)
        {
            await _customerManager.CreateCustomerDeliveryAsync(createCustomerDeliveryDto);
            return Ok();
        }
    }
}
