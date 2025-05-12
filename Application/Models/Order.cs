namespace Application.Models
{
    internal class Order
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal TotalPrice { get => Items.Sum(item => item.TotalPrice); }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
