using Application.Interfaces;
using Application.Models;

namespace Presentation.UI.Menus
{
    internal class CustomerMenu : BaseMenu
    {
        private readonly Guid _userId;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CustomerMenu(Guid userId, IProductService productService, ICartService cartService, IOrderService orderService)
            : base("What would you like to do?", ["Browse Products", "View Cart", "Manage Orders", "Show order history", "Log out", "Exit"])
        {
            _userId = userId;
            _productService = productService;
            _cartService = cartService;
            _orderService = orderService;
        }

        public void RunCustomerMenu()
        {
            while (true)
            {
                int selectedIndex = Run();

                switch (selectedIndex)
                {
                    case 0:
                        BrowseProducts();
                        break;
                    case 1:
                        ViewCart();
                        break;
                    case 2:
                        ManageOrders();
                        break;
                    case 3:
                        ShowOrderHistory();
                        break;
                    case 4:
                        LogOut();
                        return;
                    case 5:
                        Exit();
                        break;
                    default:
                        break;
                }
            }
        }

        private void BrowseProducts()
        {
            List<Product> products = _productService.GetAvailableProducts();
            if (products.Count == 0)
            {
                DisplayTitle("Browse Products");
                Console.WriteLine("Product list is empty");
                WaitForKey("\n Press any key to continiue ...");
            }
            else
            {
                ProductMenu productMenu = new ProductMenu(_productService, _cartService);
                productMenu.RunProductMenu();
            }
        }

        private void ViewCart()
        {
            CartMenu cartMenu = new CartMenu(_cartService, _orderService, _userId);
            cartMenu.RunCartMenu();
        }

        private void ManageOrders()
        {
            OrderMenu orderMenu = new OrderMenu(_orderService, _userId);
            orderMenu.RunOrderMenu();
        }

        private void ShowOrderHistory()
        {
            OrderHistoryMenu orderHistoryMenu = new OrderHistoryMenu(_orderService, _userId);
            orderHistoryMenu.RunOrderHistoryMenu();
        }

        private void LogOut()
        {
            _cartService.ClearCartOnLogout();
            _orderService.DiscardPendingOrders(_userId);
        }

        private void Exit()
        {
            LogOut();
            WaitForKey("\nPress any key to exit ...");
            Environment.Exit(0);
        }
    }
}
