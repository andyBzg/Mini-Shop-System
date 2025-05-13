namespace Application.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Product(Guid id, string name, string description, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }
    }
}
