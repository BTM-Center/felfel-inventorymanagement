namespace InventoryManagement.Core.Dtos.WrapperDtos
{
    public record ProductBatchHistoryDto
    {
        public OrderDto Order { get; set; }
        public IList<CustomerDeliveryDto> CustomerDeliveries { get; set; }
    }
}
