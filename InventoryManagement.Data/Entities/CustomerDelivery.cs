namespace InventoryManagement.Data.Entities
{
    public class CustomerDelivery
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public ProductBatch ProductBatch { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Units { get; set; }
    }
}
