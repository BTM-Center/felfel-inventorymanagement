namespace InventoryManagement.Core.Dtos
{
    public record ProductBatchDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public DateTime? CheckInDate { get; set; }
        public int Units { get; set; }
    }
}
