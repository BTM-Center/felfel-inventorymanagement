namespace InventoryManagement.Core.Dtos
{
    public record OrderDto
    {
        public int Id { get; set; }
        public SupplierDto Supplier { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public IList<OrderLineDto> OrderLines { get; set; }
    }
}
