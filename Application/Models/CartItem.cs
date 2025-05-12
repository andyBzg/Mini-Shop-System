namespace Application.Models
{
    internal class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get => Product.Price * Quantity; }
    }
}
