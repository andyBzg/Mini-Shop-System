namespace Application.Models
{
    public class Order
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid UserId { get; init; }
        public List<CartItem> Items { get; init; } = new List<CartItem>();
        public decimal TotalPrice { get => Items.Sum(item => item.TotalPrice); }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public bool IsConfirmed { get; set; } = false;

        public Order(Guid userId, List<CartItem> items)
        {
            UserId = userId;
            Items = items;
        }
    }
}
