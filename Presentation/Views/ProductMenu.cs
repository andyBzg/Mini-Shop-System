using Application.Models;
using Presentation.UI;

namespace Presentation.Views
{
    internal class ProductMenu
    {
        private readonly List<Product> _products;

        public ProductMenu(List<Product> products)
        {
            _products = products;
        }

        public void RunProductMenu()
        {
            while (true)
            {
                string prompt = "Please select a Product";
                string[] options = _products.Select(p => p.Name).ToArray();

                Menu productMenu = new Menu(prompt, options);
                int index = productMenu.Run();

                Console.Clear();
                Console.WriteLine($"Product Title: {_products[index].Name}");
                Console.WriteLine($"Description: {_products[index].Description}");
                Console.WriteLine($"Price: {_products[index].Price}");
                Console.WriteLine($"Stock: {_products[index].Stock}");
                Console.ReadKey();
                //TODO product amount input, option to add to cart
            }
        }
    }
}
