using Application.Models;

namespace Application.Interfaces
{
    public interface IOrderRepository
    {
        void SaveOrder(Order order);
        List<Order> LoadOrders();
    }
}
