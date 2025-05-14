using Application.Interfaces;
using Application.Models;
using Presentation.UI;

namespace Presentation.Views
{
    internal class ProductMenu
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public ProductMenu(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public void RunProductMenu()
        {
            while (true)
            {
                string prompt = "Please select a Product (Press ESC to go back)";
                List<Product> products = _productService.GetAvailableProducts();
                string[] options = products.Select(p => p.Name).ToArray();

                Menu productMenu = new Menu(prompt, options);
                int index = productMenu.Run();

                if (index == -1)
                    return;

                Guid selectedProductId = products[index].Id;
                Product? currentProduct = _productService.GetProductById(selectedProductId);

                if (currentProduct == null)
                {
                    Console.WriteLine("Selected product not found");
                    return;
                }

                ShowSelectedProductDetails(currentProduct);

                Console.WriteLine("\nPress ESC to go back; Enter to proceed");
                ConsoleKey keyPressed = Console.ReadKey(true).Key;

                if (keyPressed == ConsoleKey.Escape)
                    break;

                if (keyPressed == ConsoleKey.Enter)
                {
                    while (true)
                    {
                        Console.Write("\nEnter quantity to add to cart: ");
                        bool check = int.TryParse(Console.ReadLine(), out int quantity);

                        if (check && quantity > 0 && quantity <= currentProduct.Stock)
                        {
                            _cartService.AddToCart(currentProduct.Id, quantity);
                            Console.WriteLine($"Successfully added {quantity} item(s) to Shopping Cart");
                            WaitForKey("\nPress any key to continiue ...");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Quantity must be between 1 and {currentProduct.Stock}");
                            WaitForKey("\nPress any key to continiue ...");
                        }
                    }
                }
            }
        }

        private static void ShowSelectedProductDetails(Product selectedProduct)
        {
            Console.Clear();
            Console.WriteLine($"Product Title: {selectedProduct.Name}");
            Console.WriteLine($"Description: {selectedProduct.Description}");
            Console.WriteLine($"Price: {selectedProduct.Price}");
            Console.WriteLine($"Stock: {selectedProduct.Stock}");
        }

        private void WaitForKey(string? message = null)
        {
            Console.WriteLine(message);
            Console.ReadKey(true);
        }
    }
}
