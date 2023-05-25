namespace InventoryManagement.Core.Dtos.WrapperDtos
{
    public record UpdateProductBatchDto
    {
        public int Id { get; set; }
        public DateTime? CheckInDate { get; set; }
        public int Units { get; set; }
    }
}
