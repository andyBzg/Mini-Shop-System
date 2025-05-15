using Application.Models;

namespace Application.Interfaces
{
    public interface ICartService
    {
        List<CartItem> GetCartItems();
        bool AddToCart(Guid id, int quantity);
        bool RemoveFromCart(Guid id);
        void ClearCart();
    }
}
