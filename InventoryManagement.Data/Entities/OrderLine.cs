namespace InventoryManagement.Data.Entities
{
    public class OrderLine
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public ProductBatch ProductBatch { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Units { get; set; }
    }
}
