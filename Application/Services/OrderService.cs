using Application.Interfaces;
using Application.Models;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;

        public OrderService(IOrderRepository orderRepository, IProductService productService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public void AddOrder(Guid userId, List<CartItem> cartItems)
        {
            throw new NotImplementedException();
        }
    }
}
