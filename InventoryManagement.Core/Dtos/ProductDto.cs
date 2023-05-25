namespace InventoryManagement.Core.Dtos
{
    public record ProductDto
    {
        public int Id { get; set; }
        public SupplierDto Supplier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ExpiryDays { get; set; }
    }
}
