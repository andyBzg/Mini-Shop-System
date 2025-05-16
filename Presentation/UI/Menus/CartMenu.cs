using Application.Interfaces;
using Application.Models;

namespace Presentation.UI.Menus
{
    internal class CartMenu : BaseMenu
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly Guid _userId;

        public CartMenu(ICartService cartService, IOrderService orderService, Guid userId)
            : base(string.Empty, Array.Empty<string>())
        {
            _cartService = cartService;
            _orderService = orderService;
            _userId = userId;
        }

        public void RunCartMenu()
        {
            while (true)
            {
                List<CartItem> cartItems = _cartService.GetCartItems();

                if (cartItems.Count == 0)
                {
                    DisplayTitle("Shopping Cart");
                    Console.WriteLine("Cart is empty");
                    WaitForKey("\nPress any key to continiue ...");
                    return;
                }

                string cartInfo = GetTotalAmount(cartItems);
                Prompt = cartInfo + "\nPlease select Shopping Cart Item (Press ESC to go back)\n";
                Options = cartItems.Select(ci => $"{ci.Product.Name} | Amount: {ci.Quantity} | Total Price: {ci.TotalPrice} EUR").ToArray();

                int index = Run();

                if (index == -1 && cartItems.Count > 0)
                {
                    Console.WriteLine("\nWould you like to add cart items to a new order?\n(Enter to confirm; ESC to continue shopping)");
                    ConsoleKey keyPressed;
                    do
                    {
                        keyPressed = Console.ReadKey(true).Key;

                        if (keyPressed == ConsoleKey.Escape)
                            break;
                        else if (keyPressed == ConsoleKey.Enter)
                        {
                            _orderService.AddOrder(_userId, cartItems);
                            _cartService.ClearCart();
                            Console.WriteLine("Sucessfully added cart items to order!");
                            WaitForKey("\nPress any key to continiue ...");
                        }
                    }
                    while (keyPressed != ConsoleKey.Escape && keyPressed != ConsoleKey.Enter);
                }

                if (index == -1)
                    break;

                ShowItemDetails(cartItems[index]);
                HandleCartItemAction(cartItems[index]);
                WaitForKey("\nPress any key to continiue ...");
            }
        }

        private string GetTotalAmount(List<CartItem> cartItems)
        {
            decimal totalPrice = 0;
            int amountItems = 0;
            for (int i = 0; i < cartItems.Count; i++)
            {
                totalPrice = decimal.Add(totalPrice, cartItems[i].TotalPrice);
                amountItems += cartItems[i].Quantity;
            }
            return $"Items in cart: {amountItems} \nTotal price: {totalPrice}\n";
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
            DisplayTitle("Details");
            Console.WriteLine($"Product Title: {cartItem.Product.Name}");
            Console.WriteLine($"Description: {cartItem.Product.Description}");
            Console.WriteLine($"Price: {cartItem.Product.Price} EUR");
            Console.WriteLine($"Amount: {cartItem.Quantity}");
            Console.WriteLine("------------------------");
            Console.WriteLine($"Total Price: {cartItem.TotalPrice} EUR");
        }
    }
}
