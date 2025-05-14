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
                string prompt = "Please select a Product";
                List<Product> products = _productService.GetAvailableProducts();
                string[] options = products.Select(p => p.Name).ToArray();

                Menu productMenu = new Menu(prompt, options);
                int index = productMenu.Run();

                Guid selectedProductId = products[index].Id;
                Product? currentProduct = _productService.GetProductById(selectedProductId);

                if (currentProduct == null)
                {
                    Console.WriteLine("Selected product not found");
                    return;
                }

                bool check;
                int quantity;

                ShowSelectedProductDetails(currentProduct);
                while (true)
                {
                    Console.Write("\nEnter quantity to add to cart: ");
                    check = int.TryParse(Console.ReadLine(), out quantity);

                    if (check && quantity >= 0 && quantity <= currentProduct.Stock)
                        break;
                    else
                        Console.WriteLine($"Quantity must be 0 or {currentProduct.Stock}");

                    Console.ReadKey();
                }

                bool isItemAdded = _cartService.AddToCart(currentProduct.Id, quantity);
                if (isItemAdded)
                    Console.WriteLine($"Successfully added {quantity} item(s) to Shopping Cart");
                else if (quantity == 0)
                    Console.WriteLine("Nothing added");

                Console.ReadKey();
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
    }
}
