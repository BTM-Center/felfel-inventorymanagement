namespace InventoryManagement.Data.Entities
{
    public class ProductBatch
    {
        public int Id { get; set; }
        public DateTime? CheckInDate { get; set; }
        public int Units { get; set; }
        public Product Product { get; set; }
    }
}
