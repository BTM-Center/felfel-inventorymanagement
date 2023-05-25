namespace InventoryManagement.Core.Dtos.WrapperDtos
{
    public record CreateOrderLineDto
    {
        public int ProductId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Units { get; set; }
    }
}
