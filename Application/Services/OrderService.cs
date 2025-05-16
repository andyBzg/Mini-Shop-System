using Application.Interfaces;
using Application.Models;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private List<Order> _pendingOrders = new List<Order>();

        public OrderService(IOrderRepository orderRepository, IProductService productService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public bool AddOrder(Guid userId, List<CartItem> cartItems)
        {
            Order order = new Order(userId, cartItems);
            _pendingOrders.Add(order);
            return true;
        }

        public void DiscardPendingOrders(Guid userId)
        {
            List<Order> orders = GetPendingOrders(userId);

            if (orders.Count != 0)
            {
                for (int i = 0; i < orders.Count; i++)
                {
                    for (int j = 0; j < orders[i].Items.Count; j++)
                    {
                        Guid productId = orders[i].Items[j].Product.Id;
                        int quantity = orders[i].Items[j].Quantity;
                        _productService.IncreaseProductQuantity(productId, quantity);
                    }
                }
            }
        }

        public bool ConfirmOrderById(Guid orderId)
        {
            Order? pendingOrder = _pendingOrders.FirstOrDefault(po => po.Id == orderId);
            
            if (pendingOrder == null) 
                return false;

            pendingOrder.IsConfirmed = true;
            _orderRepository.Save(pendingOrder);
            _pendingOrders.Remove(pendingOrder);
            return true;
        }

        public Order? GetOrderById(Guid orderId)
        {
            return _orderRepository.GetById(orderId);
        }

        public List<Order> GetPendingOrders(Guid userId)
        {
            return _pendingOrders.Where(po => po.UserId == userId).ToList();
        }

        public List<Order> GetConfirmedOrdersByUser(Guid userId)
        {
            List<Order> userOrders = _orderRepository.LoadAll().Where(o => o.UserId == userId).ToList();
            List<Order> confirmedOrders = userOrders.Where(o => o.IsConfirmed == true).ToList();

            if (confirmedOrders.Count > 0)
                return confirmedOrders;
            else
                return new List<Order>();
        }
    }
}
