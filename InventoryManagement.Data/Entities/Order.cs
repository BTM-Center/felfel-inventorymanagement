namespace InventoryManagement.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public IList<OrderLine> OrderLines { get; set; }
    }
}
