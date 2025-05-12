namespace Application.Models
{
    internal class OrderItem
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal PriceAtPurcase { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get => PriceAtPurcase * Quantity; }
    }
}
