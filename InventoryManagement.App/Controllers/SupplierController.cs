using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Core.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Shared.Constants.Constants;

namespace InventoryManagement.App.Controllers
{
    [ApiController]
    [Route(ApiRoutes.ControllerName)]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierManager _supplierManager;

        public SupplierController(ISupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
        }

        [HttpGet(ApiRoutes.GetOrder)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var orderDto = await _supplierManager.GetOrderAsync(id);
            return orderDto == null ? NotFound() : Ok(orderDto);
        }

        [HttpPost(ApiRoutes.CreateOrder)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var orderId = await _supplierManager.CreateOrderAsync(createOrderDto);
            return Created(nameof(CreateOrderAsync), orderId);
        }
    }
}
