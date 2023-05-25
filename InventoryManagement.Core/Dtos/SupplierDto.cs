namespace InventoryManagement.Core.Dtos
{
    public record SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IList<ProductDto> Products { get; set; }
    }
}
