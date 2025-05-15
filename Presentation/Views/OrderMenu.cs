using Application.Interfaces;

namespace Presentation.Views
{
    internal class OrderMenu
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly Guid _userId;

        public OrderMenu(IOrderService orderService, IProductService productService, Guid userId)
        {
            _orderService = orderService;
            _productService = productService;
            _userId = userId;
        }

        public void RunOrderMenu()
        {
            //TODO implement order menu
        }
    }
}
