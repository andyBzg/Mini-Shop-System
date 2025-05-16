using Application.Models;

namespace Application.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> LoadAll();
        Order? GetById(Guid id);
        void Save(Order order);
        void Update(Order order);
    }
}
