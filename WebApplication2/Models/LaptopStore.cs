namespace WebApplication2.Models
{
    public class LaptopStore
    {
        public Guid Id { get; set; }
        public Store Store { get; set; }
        public Guid StoreNumber { get; set; }

        public Laptop Laptops { get; set; }
        public Guid LaptopsNumber { get; set; }
        public int Quantity { get; set; }
    }
}
