using Application.Models;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        bool AddOrder(Guid userId, List<CartItem> cartItems);
        List<Order> GetPendingOrders(Guid userId);
        List<Order> GetConfirmedOrdersByUser(Guid userId);
        Order? GetOrderById(Guid orderId);
        bool ConfirmOrderById(Guid orderId);
        void DiscardPendingOrders(Guid userId);
    }
}
