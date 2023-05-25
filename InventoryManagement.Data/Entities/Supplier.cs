namespace InventoryManagement.Data.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IList<Product> Products { get; set; }
    }
}
