namespace InventoryManagement.Core.Dtos
{
    public record OrderLineDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public ProductBatchDto ProductBatch { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Units { get; set; }
    }
}
