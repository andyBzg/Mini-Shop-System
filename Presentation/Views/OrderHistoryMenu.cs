using Application.Interfaces;
using Application.Models;
using Presentation.UI;

namespace Presentation.Views
{
    internal class OrderHistoryMenu
    {
        private readonly IOrderService _orderService;
        private readonly Guid _userId;

        public OrderHistoryMenu(IOrderService orderService, Guid userId)
        {
            _orderService = orderService;
            _userId = userId;
        }

        internal void RunOrderHistoryMenu()
        {
            while (true)
            {
                List<Order> orders = _orderService.GetConfirmedOrdersByUser(_userId);

                if (orders.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Order history is empty");
                    WaitForKey("\nPress any key to continiue ...");
                    return;
                }

                string prompt = "Please select Order from List below (Press ESC to go back)\n";
                string[] options = orders.Select((po, i) => $"Order #{i + 1}: {po.TotalPrice} EUR").ToArray();

                Menu historyMenu = new Menu(prompt, options);
                int index = historyMenu.Run();

                if (index == -1)
                    break;

                Order selectedOrder = orders[index];
                ShowOrderDetails(selectedOrder);
                WaitForKey("\nPress any key to continiue ...");
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
