using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Core.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Shared.Constants.Constants;

namespace InventoryManagement.App.Controllers
{
    [ApiController]
    [Route(ApiRoutes.ControllerName)]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet(ApiRoutes.GetProductBatch)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductBatchDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductBatchAsync(int id)
        {
            var productBatchDto = await _productManager.GetProductBatchAsync(id);
            return productBatchDto == null ? NotFound() : Ok(productBatchDto);
        }

        [HttpGet(ApiRoutes.GetBatchesForProduct)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ProductBatchDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBatchesForProductAsync(int productId)
        {
            var productBatchDtos = await _productManager.GetBatchesForProductAsync(productId);
            return productBatchDtos == null ? NotFound() : Ok(productBatchDtos);
        }

        [HttpGet(ApiRoutes.GetAvailableProductBatchesWithFreshnessStatus)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ProductBatchWithFreshnessStatusDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAvailableProductBatchesWithFreshnessStatusAsync()
        {
            var productBatchesWithFreshnessStatusDtos = await _productManager.GetAvailableProductBatchesWithFreshnessStatusAsync();
            return productBatchesWithFreshnessStatusDtos == null ? NotFound() : Ok(productBatchesWithFreshnessStatusDtos);
        }

        [HttpGet(ApiRoutes.GetProductBatchHistory)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductBatchHistoryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductBatchHistoryAsync(int id)
        {
            var productBatchHistoryDto = await _productManager.GetProductBatchHistoryAsync(id);
            return productBatchHistoryDto == null ? NotFound() : Ok(productBatchHistoryDto);
        }

        [HttpPost(ApiRoutes.UpdateProductBatch)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductBatchAsync(UpdateProductBatchDto updateProductBatchDto)
        {
            var status = await _productManager.UpdateProductBatchAsync(updateProductBatchDto);
            return Ok(status);
        }
    }
}
