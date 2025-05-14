using Application.Interfaces;
using Application.Models;
using Presentation.UI;

namespace Presentation.Views
{
    internal class CustomerMenu
    {
        private readonly User _user;
        private readonly IProductService _productService;

        public CustomerMenu(User user, IProductService productService)
        {
            _user = user;
            _productService = productService;
        }

        public void RunCustomerMenu()
        {
            while (true)
            {
                string prompt = $"Welcome {_user.Username}! What would you like to do?";
                string[] options = { "Browse Products", "View Cart", "Place Order", "Back", "Exit" };

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
            }
            else
            {
                ProductMenu productMenu = new ProductMenu(products);
                productMenu.RunProductMenu();
            }

            Console.ReadKey();
        }

        private void ViewCart()
        {
            //TODO implement logic to view customer cart 
            Console.WriteLine("Cart contents...");
            Console.ReadKey();
        }

        private void PlaceOrder()
        {
            //TODO implement logic to place an order 
            Console.WriteLine("Order placed!");
            Console.ReadKey();
        }

        private void Exit()
        {
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
