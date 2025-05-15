using Application.Models;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(Guid userId, List<CartItem> cartItems);
    }
}
