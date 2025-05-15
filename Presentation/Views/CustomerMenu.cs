using Application.Interfaces;
using Application.Models;
using Presentation.UI;

namespace Presentation.Views
{
    internal class CustomerMenu
    {
        private readonly User _user;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CustomerMenu(User user, IProductService productService, ICartService cartService)
        {
            _user = user;
            _productService = productService;
            _cartService = cartService;
        }

        public void RunCustomerMenu()
        {
            while (true)
            {
                string prompt = $"Welcome {_user.Username}! What would you like to do?";
                string[] options = { "Browse Products", "View Cart", "Place Order", "Log out", "Exit" };

                Menu adminMenu = new Menu(prompt, options);
                int selectedIndex = adminMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        BrowseProducts();
                        break;
                    case 1:
                        ViewCart();
                        break;
                    case 2:
                        PlaceOrder();
                        break;
                    case 3:
                        LogOut();
                        return;
                    case 4:
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
            CartMenu cartMenu = new CartMenu(_cartService);
            cartMenu.RunCartMenu();
        }

        private void PlaceOrder()
        {
            //TODO implement logic to place an order 
            Console.WriteLine("Order placed!");
            Console.ReadKey();
        }

        private void LogOut()
        {
            List<CartItem> cartItems = _cartService.GetCartItems();

            if (cartItems.Count != 0)
            {
                for (int i = 0; i < cartItems.Count; i++)
                {
                    _cartService.RemoveFromCart(cartItems[i].Product.Id);
                }
            }
        }

        private void Exit()
        {
            LogOut();
            WaitForKey("\nPress any key to exit ...");
            Environment.Exit(0);
        }

        private void WaitForKey(string? message = null)
        {
            Console.WriteLine(message);
            Console.ReadKey(true);
        }
    }
}
