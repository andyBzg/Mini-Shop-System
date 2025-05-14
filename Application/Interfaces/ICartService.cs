using Application.Models;

namespace Application.Interfaces
{
    public interface ICartService
    {
        bool AddToCart(Guid id, int quantity);
        List<CartItem> GetCartItems();
    }
}
