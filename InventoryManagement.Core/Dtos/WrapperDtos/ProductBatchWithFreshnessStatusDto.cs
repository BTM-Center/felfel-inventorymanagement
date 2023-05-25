using Shared.Enums;

namespace InventoryManagement.Core.Dtos.WrapperDtos
{
    public record ProductBatchWithFreshnessStatusDto
    {
        public ProductBatchDto ProductBatch { get; set; }
        public ProductBatchFreshnessStatus FreshnessStatus { get; set; }
    }
}
