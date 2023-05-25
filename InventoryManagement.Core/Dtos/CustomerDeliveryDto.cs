namespace InventoryManagement.Core.Dtos
{
    public record CustomerDeliveryDto
    {
        public int Id { get; set; }
        public CustomerDto Customer { get; set; }
        public ProductBatchDto ProductBatch { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Units { get; set; }
    }
}
