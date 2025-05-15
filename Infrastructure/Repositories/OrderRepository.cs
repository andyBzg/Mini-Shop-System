using Application.Interfaces;
using Application.Models;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _filePath;

        public OrderRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Order> LoadOrders()
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
