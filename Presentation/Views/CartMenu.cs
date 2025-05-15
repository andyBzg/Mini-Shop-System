using Application.Interfaces;
using Application.Models;
using Presentation.UI;
using System;

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

                ShowItemDetails(cartItems[index]);
                HandleCartItemAction(cartItems[index]);
                WaitForKey("\nPress any key to continiue ...");
            }
        }

        private void HandleCartItemAction(CartItem cartItem)
        {
            Console.WriteLine("\nRemove this item from Shopping Cart? \n(Press ENTER to confirm; ESC to go back)");
            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            if (keyPressed == ConsoleKey.Escape)
                return;

            if (keyPressed == ConsoleKey.Enter)
            {
                bool success = _cartService.RemoveFromCart(cartItem.Product.Id);

                if (success)
                    Console.WriteLine($"{cartItem.Product.Name} removed from the cart");
                else
                    Console.WriteLine("Failed to remove item");
            }
        }

        private void ShowItemDetails(CartItem cartItem)
        {
            Console.Clear();
            Console.WriteLine($"Product Title: {cartItem.Product.Name}");
            Console.WriteLine($"Description: {cartItem.Product.Description}");
            Console.WriteLine($"Price: {cartItem.Product.Price}");
            Console.WriteLine($"Amount: {cartItem.Quantity}");
            Console.WriteLine("------------------------");
            Console.WriteLine($"Total Price: {cartItem.TotalPrice} EUR");
        }

        private void WaitForKey(string? message = null)
        {
            Console.WriteLine(message);
            Console.ReadKey(true);
        }
    }
}
