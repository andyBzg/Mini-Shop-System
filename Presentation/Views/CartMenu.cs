using Application.Interfaces;
using Application.Models;
using Presentation.UI;

namespace Presentation.Views
{
    internal class CartMenu
    {
        private readonly ICartService _cartService;

        public CartMenu(ICartService cartService)
        {
            _cartService = cartService;
        }

        public void RunCartMenu()
        {
            while (true)
            {
                string prompt = "Please select Shopping Cart Item (Press ESC to go back)";
                List<CartItem> cartItems = _cartService.GetCartItems();
                string[] options = cartItems.Select(ci => $"{ci.Product.Name} | Amount: {ci.Quantity} | Total Price: {ci.TotalPrice}").ToArray();
                
                if (cartItems.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Shopping cart is empty");
                    WaitForKey("\nPlress any key to continiue ...");
                    return;
                }            

                Menu cartMenu = new Menu(prompt, options);
                int index = cartMenu.Run();

                if (index == -1)
                    return;
            }
        }

        private void WaitForKey(string? message = null)
        {
            Console.WriteLine(message);
            Console.ReadKey(true);
        }
    }
}
