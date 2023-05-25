namespace InventoryManagement.Core.Dtos
{
    public record CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
