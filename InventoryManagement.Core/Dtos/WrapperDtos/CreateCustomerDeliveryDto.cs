namespace InventoryManagement.Core.Dtos.WrapperDtos
{
    public record CreateCustomerDeliveryDto
    {
        public int CustomerId { get; set; }
        public int ProductBatchId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Units { get; set; }
    }
}
