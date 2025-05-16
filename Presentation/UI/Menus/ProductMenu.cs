using Application.Interfaces;
using Application.Models;

namespace Presentation.UI.Menus
{
    internal class ProductMenu : BaseMenu
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public ProductMenu(IProductService productService, ICartService cartService)
            : base("Please select a Product (Press ESC to go back)", Array.Empty<string>())
        {
            _productService = productService;
            _cartService = cartService;
        }

        public void RunProductMenu()
        {
            while (true)
            {
                List<Product> products = _productService.GetAvailableProducts();

                if (products.Count == 0)
                {
                    DisplayTitle("Available products");
                    Console.WriteLine("No available products");
                    WaitForKey("\nPress any key to continiue ...");
                    break;
                }

                Options = products.Select(p => p.Name).ToArray();
                int index = Run();

                if (index == -1)
                    break;

                Guid selectedProductId = products[index].Id;
                Product? selectedProduct = _productService.GetProductById(selectedProductId);

                if (selectedProduct == null)
                {
                    Console.WriteLine("Selected product not found");
                    WaitForKey("\nPress any key to continiue ...");
                    break;
                }

                ShowSelectedProductDetails(selectedProduct);
                HandleProductAction(selectedProduct);
            }
        }

        private void HandleProductAction(Product product)
        {
            Console.WriteLine("\nWould you like to add this Product to Shopping Cart? \n(Press ENTER to confirm; ESC to go back)");
            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            if (keyPressed == ConsoleKey.Escape)
                return;

            if (keyPressed == ConsoleKey.Enter)
            {
                while (true)
                {
                    Console.Write("\nEnter quantity to add to cart: ");
                    bool check = int.TryParse(Console.ReadLine(), out int quantity);

                    if (check && quantity > 0 && quantity <= product.Stock)
                    {
                        bool sucess = _cartService.AddToCart(product.Id, quantity);

                        if (sucess)
                            Console.WriteLine($"Successfully added {quantity} item(s) to Shopping Cart");
                        else
                            Console.WriteLine("Failed to add item");

                        WaitForKey("\nPress any key to continiue ...");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Quantity must be between 1 and {product.Stock}");
                        WaitForKey("\nPress any key to continiue ...");
                    }
                }
            }
        }

        private void ShowSelectedProductDetails(Product selectedProduct)
        {
            DisplayTitle("Details");
            Console.WriteLine($"Product Title: {selectedProduct.Name}");
            Console.WriteLine($"Description: {selectedProduct.Description}");
            Console.WriteLine($"Price: {selectedProduct.Price} EUR");
            Console.WriteLine($"Stock: {selectedProduct.Stock}");
        }
    }
}
