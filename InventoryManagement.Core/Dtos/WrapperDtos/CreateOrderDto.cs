namespace InventoryManagement.Core.Dtos.WrapperDtos
{
    public record CreateOrderDto
    {
        public int SupplierId { get; set; }
        public IList<CreateOrderLineDto> OrderLines { get; set; }
    }
}
