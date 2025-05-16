using Application.Interfaces;
using Application.Models;
using Presentation.UI;

namespace Presentation.Views
{
    internal class OrderMenu
    {
        private readonly IOrderService _orderService;
        private readonly Guid _userId;

        public OrderMenu(IOrderService orderService, Guid userId)
        {
            _orderService = orderService;
            _userId = userId;
        }

        public void RunOrderMenu()
        {
            while (true)
            {
                List<Order> pendingOrders = _orderService.GetPendingOrders(_userId);

                if (pendingOrders.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("You have no pending orders.");
                    WaitForKey("\nPress any key to continiue ...");
                    return;
                }

                string prompt = "Please select Order from List below (Press ESC to go back)\n";
                string[] options = pendingOrders.Select((po, i) => $"Order #{i + 1}: {po.TotalPrice} EUR").ToArray();

                Menu orderMenu = new Menu(prompt, options);
                int index = orderMenu.Run();

                if (index == -1)
                    break;

                Order selectedOrder = pendingOrders[index];
                ShowOrderDetails(selectedOrder);
                HandleOrderAction(selectedOrder);
                WaitForKey("\nPress any key to continiue ...");
            }
        }

        private void HandleOrderAction(Order order)
        {
            Console.WriteLine("\nWould you like to place selected Order? \n(Press ENTER to confirm; ESC to go back)");
            ConsoleKey keyPressed = Console.ReadKey(true).Key;

            if (keyPressed == ConsoleKey.Escape)
                return;

            if (keyPressed == ConsoleKey.Enter)
            {
                bool success = _orderService.ConfirmOrderById(order.Id);

                if (success)
                    Console.WriteLine($"Order placed!");
                else
                    Console.WriteLine("Failed to place order");
            }
        }

        private void ShowOrderDetails(Order order)
        {
            List<CartItem> items = order.Items;
            string status = order.IsConfirmed == false ? "The order is waiting to be placed." : "The order has been placed.";

            Console.Clear();
            Console.WriteLine("List of Products:\n");
            items.ForEach(item => Console.WriteLine($"{item.Product.Name} | Amount: {item.Quantity} | Price: {item.TotalPrice} EUR"));
            Console.WriteLine("------");
            Console.WriteLine($"Total Price: {order.TotalPrice} EUR");
            Console.WriteLine("------");
            Console.WriteLine($"Time created: {order.CreatedAt.ToString("dd.MM.yyyy | HH:mm")}");
            Console.WriteLine($"Order status: {status}");
        }

        private void WaitForKey(string? message = null)
        {
            Console.WriteLine(message);
            Console.ReadKey(true);
        }
    }
}
